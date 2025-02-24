using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Xml;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Models.QRCode;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class BarcodeSerialQRRepositories : IBarcodeSerialQRRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IPrinterService _printerService;
        public readonly IErrorRepositories _errorRepositories;

        public BarcodeSerialQRRepositories(IConfiguration Configuration, DBWARContext context, IPrinterService printerService, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _printerService = printerService;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> AddBarcodeSerialQRTSJ(BarcodeSerialQr parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            BarcodeSerialQr request = new BarcodeSerialQr();

            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {

                    request.SerialQrId = parameter.SerialQrId;
                    request.SerialCode = parameter.SerialCode;
                    request.RegistrationCode = parameter.RegistrationCode;
                    request.Source = "TSJ";
                    request.TotalPrint = 1;
                    request.CreatedAt = DateTime.Now;
                    request.CreatedBy = parameter.CreatedBy;
                    request.UpdatedAt = DateTime.Now;
                    _context.BarcodeSerialQrs.Add(request);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();
                    dbTrans.Dispose();

                    res.Code = 200;
                    res.Message = MessageRepositories.MessageSuccess + " Create Barcode Serial QR ";
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

        public async Task<GlobalObjectResponse> AddBarcodeSerialQRInoac(BarcodeSerialQr parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            BarcodeSerialQr request = new BarcodeSerialQr();

            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    request.SerialQrId = parameter.SerialQrId;
                    request.SerialCode = parameter.SerialCode;
                    request.RegistrationCode = parameter.RegistrationCode;
                    request.Source = "INOAC";
                    request.TotalPrint = 1;
                    request.CreatedAt = DateTime.Now;
                    request.CreatedBy = parameter.CreatedBy;
                    request.UpdatedAt = DateTime.Now;
                    _context.BarcodeSerialQrs.Add(request);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();
                    dbTrans.Dispose();

                    res.Code = 200;
                    res.Message = MessageRepositories.MessageSuccess + " Create Barcode Serial QR ";
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

                        _addError.ServiceName = "AddBarcodeSerialQRInoac";
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

                        _addError.ServiceName = "AddBarcodeSerialQRInoac";
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

                    _addError.ServiceName = "AddBarcodeSerialQRInoac";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

            }
        }

        public async Task<GlobalObjectResponse> DeleteSerialCode(string SerialCode, string? Source, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();

            try
            {

                var dataSerialCodeTSJ = _context.BarcodeSerialQrs.Where(x => x.SerialCode == SerialCode && x.Source == Source).FirstOrDefault();
                if(dataSerialCodeTSJ != null)
                {
                    dataSerialCodeTSJ.Active = false;
                    _context.BarcodeSerialQrs.Update(dataSerialCodeTSJ);
                    await _context.SaveChangesAsync();
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Delete Barcode Serial QR.";
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

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
               
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

       
        public async Task<GlobalObjectListResponse> ListDataBarcodeSerialQR(string? SerialCode, string? Source, bool? SelectDate, DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<BarcodeSerialQr> lst_barcode_serial = new List<BarcodeSerialQr>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                if (SerialCode != "" && SerialCode != null)
                {
                    lst_barcode_serial = _context.BarcodeSerialQrs.Where(x => x.SerialCode == SerialCode && x.Source == Source && x.Active == true).OrderBy(x => x.Id).AsNoTracking().ToList();
                }

                if (SelectDate == true)
                {
                    lst_barcode_serial = _context.BarcodeSerialQrs.Where(x => x.CreatedAt >= Convert.ToDateTime(createdAtFrom) && x.CreatedAt <= Convert.ToDateTime(createdAtTo) && x.Active == true).OrderBy(x => x.Id).AsNoTracking().ToList();
                }

                res.Code = 200;
                res.Data = lst_barcode_serial.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Barcode Serial QR."; ;
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

                    _addError.ServiceName = "ListDataBarcodeSerialQR";
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

                    _addError.ServiceName = "ListDataBarcodeSerialQR";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataBarcodeSerialQR";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }

        public async Task<GlobalObjectResponse> RePrintSerialNumberQR(string? SerialCode, string RegistrationCode, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            ActivationQr request = new ActivationQr();

            string printName = "";

            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {

                    var set_printer = _context.SettingPrinters.Where(x => x.PrinterValue == "PrintLabelWarrantyTSJ").AsNoTracking().FirstOrDefault();
                    if (set_printer != null)
                    {

                        printName = set_printer.PrinterName;
                    }
                    else
                    {

                        res.Code = 500;
                        res.Message = MessageRepositories.MessageFailed + " Print Name Empty.";
                        res.Error = true;
                        return res;
                    }

                    var rePrint = _context.BarcodeSerialQrs.Where(x => x.SerialCode == SerialCode && x.RegistrationCode == RegistrationCode).FirstOrDefault();
                    if (rePrint != null)
                    {
                        rePrint.UpdatedAt = DateTime.Now;
                        _context.BarcodeSerialQrs.Update(rePrint);
                        await _context.SaveChangesAsync();

                        await _printerService.PrintBarcodeSerialQRExist(SerialCode, RegistrationCode, rePrint.CreatedAt, printName, rePrint.Source, cancellationToken);
                    }

                    dbTrans.Commit();
                    dbTrans.Dispose();

                    res.Code = 200;
                    res.Message = MessageRepositories.MessageSuccess + " Re Print Serial Number QR ";
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
                        return res;
                    }
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                    res.Error = true;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                    return res;
                }

            }
        }
    }
}
