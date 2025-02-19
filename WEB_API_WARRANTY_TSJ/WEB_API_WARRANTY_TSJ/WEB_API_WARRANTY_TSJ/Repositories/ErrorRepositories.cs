using Microsoft.EntityFrameworkCore;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class ErrorRepositories : IErrorRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;

        public ErrorRepositories(IConfiguration Configuration, DBWARContext context)
        {
            _configuration = Configuration;
            _context = context;
        }

        public async Task<GlobalObjectResponse> AddLogError(LogError parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogWarranty request = new LogWarranty();
            try
            {
                parameter.ErrorDate = DateTime.Now;
                _context.LogErrors.Add(parameter);
                await _context.SaveChangesAsync();

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Create Log Error.";
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

        public async Task<GlobalObjectListResponse> ListDataError(DateTime? errorDateFrom, DateTime? errorDateTo, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<LogError> lst_error = new List<LogError>();
            try
            {
                System.GC.Collect();
                lst_error = _context.LogErrors.Where(x => x.ErrorDate >= errorDateFrom && x.ErrorDate <= errorDateTo).OrderByDescending(x => x.ErrorDate).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_error.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Error."; ;
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
