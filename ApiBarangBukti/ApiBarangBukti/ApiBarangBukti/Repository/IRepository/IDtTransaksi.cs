using ApiBarangBukti.Help;
using ApiBarangBukti.Models;

namespace ApiBarangBukti.Repository.IRepository
{
    public interface IDtTransaksi
    {
        Task<GlobalObjectResponse> AddDtTransaksi(DtTransaksi parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateDtTransaksi(DtTransaksi parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataDtTransaksi(string IdTransaksi, string NoPerkara, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> GetPreviewFile(int Id, CancellationToken cancellationToken);
    }
}
