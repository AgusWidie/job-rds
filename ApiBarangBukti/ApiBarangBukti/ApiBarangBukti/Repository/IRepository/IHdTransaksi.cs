using ApiBarangBukti.Help;
using ApiBarangBukti.Models;

namespace ApiBarangBukti.Repository.IRepository
{
    public interface IHdTransaksi
    {
        Task<GlobalObjectResponse> AddHdTransaksi(HdTransaksi parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateHdTransaksi(HdTransaksi parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdTransaksi(CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdTransaksiById(string IdTransaksi, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> GetPreviewFile(string IdHdTransaksi, CancellationToken cancellationToken);
    }
}
