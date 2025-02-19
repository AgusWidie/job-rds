using ApiBarangBukti.Help;
using ApiBarangBukti.Models;

namespace ApiBarangBukti.Repository.IRepository
{
    public interface ILogUser
    {
        Task<GlobalObjectResponse> AddLogUser(LogAktivitasUser parameter, CancellationToken cancellationToken);
    }
}
