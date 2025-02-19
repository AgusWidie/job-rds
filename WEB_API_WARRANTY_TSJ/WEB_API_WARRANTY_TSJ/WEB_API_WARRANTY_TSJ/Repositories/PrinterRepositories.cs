using Microsoft.EntityFrameworkCore;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class PrinterRepositories : IPrinterRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IErrorRepositories _errorRepositories;
        public PrinterRepositories(IConfiguration Configuration, DBWARContext context, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> GetDataPrinterName(string? PrinterValue, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            List<SettingPrinter> printer_data = new List<SettingPrinter>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                printer_data = _context.SettingPrinters.Where(x => x.PrinterValue == PrinterValue).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = printer_data.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Printer Name."; ;
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

                    _addError.ServiceName = "GetDataPrinterName";
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

                    _addError.ServiceName = "GetDataPrinterName";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "GetDataPrinterName";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }
    }
}
