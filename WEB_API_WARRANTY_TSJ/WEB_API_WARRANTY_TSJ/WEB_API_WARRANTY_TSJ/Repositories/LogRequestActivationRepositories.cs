using Microsoft.EntityFrameworkCore;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class LogRequestActivationRepositories : ILogRequestActivationQRRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IPrinterService _printerService;
        public readonly IErrorRepositories _errorRepositories;

        public LogRequestActivationRepositories(IConfiguration Configuration, DBWARContext context, IPrinterService printerService, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _printerService = printerService;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> AddLogRequestActivation(LogRequestActivation parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                _context.LogRequestActivations.Add(parameter);
                await _context.SaveChangesAsync();

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Create Log Request Activation QR.";
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

        public async Task<GlobalObjectListResponse> ListDataActivationQR(DateTime? activationDateFrom, DateTime? activationDateTo, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<LogRequestActivation> lst_activation_qr = new List<LogRequestActivation>();
            try
            {
                System.GC.Collect();
                lst_activation_qr = _context.LogRequestActivations.Where(x => x.CreatedAt >= activationDateFrom && x.CreatedAt <= activationDateTo).OrderByDescending(x => x.CreatedAt).AsNoTracking().ToList();

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
                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }
        }
    }
}
