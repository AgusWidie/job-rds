using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Models.QRCode;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class ActivationQRRepositories : IActivationQRRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IPrinterService _printerService;
        public readonly IErrorRepositories _errorRepositories;

        public ActivationQRRepositories(IConfiguration Configuration, DBWARContext context, IPrinterService printerService, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _printerService = printerService;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> AddActivationQR(ActivationQrRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            ActivationQr request = new ActivationQr();
            ConcurrentQueue<ActivationQrRequest> dataActivationQR = new ConcurrentQueue<ActivationQrRequest>();

            string printName = "";

            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {

                    var set_printer = _context.SettingPrinters.Where(x => x.PrinterValue == "PrintLabelActivationCodeWarrantyTSJ").AsNoTracking().FirstOrDefault();
                    if (set_printer != null) {

                        printName = set_printer.PrinterName;

                    } else {

                        res.Code = 500;
                        res.Message = MessageRepositories.MessageFailed + " Print Name Empty.";
                        res.Error = true;
                        return res;
                    }

                    dataActivationQR.Enqueue(parameter);
                    while (!dataActivationQR.IsEmpty)
                    {
                        dataActivationQR.TryDequeue(out parameter);

                        int i = 1;
                        while (i <= parameter.TotalPrint)
                        {

                            request.ActivationCode = DateTime.Now.Ticks.ToString().ToUpper();
                            request.CreatedAt = DateTime.Now;
                            request.CreatedBy = parameter.CreatedBy;
                            request.UpdatedAt = DateTime.Now;
                            _context.ActivationQrs.Add(request);
                            await _context.SaveChangesAsync();

                            await _printerService.PrintActivationQR(request, printName, cancellationToken);

                            i++;
                        }
                    }


                    dbTrans.Commit();
                    dbTrans.Dispose();

                    res.Code = 200;
                    res.Message = MessageRepositories.MessageSuccess + " Create Activation Code QR ";
                    res.Error = false;
                    return res;
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    if (ex.InnerException.Message != null)
                    {
                        res.Code = 500;
                        res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                        res.Error = true;

                        var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddBarcodeSerialQRTSJ";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                    res.Error = true;

                    return res;
                }

                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    string jsonStr = "";
                    if (ex.InnerException.Message != null)
                    {
                        res.Code = 500;
                        res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                        res.Error = true;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddBarcodeSerialQRTSJ";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "AddBarcodeSerialQRTSJ";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

            }
        }

        public async Task<GlobalObjectListResponse> ListDataActionCodeQR(string? ActionCode, bool? SelectDate, DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<ActivationQr> lst_activation_qr = new List<ActivationQr>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                if (ActionCode != "" && ActionCode != null)
                {
                    lst_activation_qr = _context.ActivationQrs.Where(x => x.ActivationCode == ActionCode).OrderBy(x => x.Id).AsNoTracking().ToList();
                }

                if (SelectDate == true)
                {
                    lst_activation_qr = _context.ActivationQrs.Where(x => x.CreatedAt >= Convert.ToDateTime(createdAtFrom) && x.CreatedAt <= Convert.ToDateTime(createdAtTo)).OrderBy(x => x.Id).AsNoTracking().ToList();
                }

                res.Code = 200;
                res.Data = lst_activation_qr.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Activation QR."; ;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataActionCodeQR";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataActionCodeQR";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataActionCodeQR";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }
    }
}
