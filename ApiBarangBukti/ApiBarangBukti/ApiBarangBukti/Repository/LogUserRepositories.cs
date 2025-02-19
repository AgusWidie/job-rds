using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBarangBukti.Repository
{
    public class LogUserRepositories : ILogUser
    {
        public readonly IConfiguration _configuration;
        public readonly DbsiramContext _context;

        public LogUserRepositories(IConfiguration Configuration, DbsiramContext context)
        {
            _configuration = Configuration;
            _context = context;
        }

        public async Task<GlobalObjectResponse> AddLogUser(LogAktivitasUser parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                parameter.CreateAt = DateTime.Now;
                parameter.UpdateAt = DateTime.Now;
                _context.LogAktivitasUsers.Add(parameter);
                await _context.SaveChangesAsync();

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess;
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
