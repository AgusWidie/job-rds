using WebBarangBukti.Help;
using WebBarangBukti.Models;

namespace WebBarangBukti.Service.IService
{
    public interface IHdTransaksiService
    {
        Task<GlobalObjectResponse> AddHdTransaksi(HdTransaksi parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateHdTransaksi(HdTransaksi parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdTransaksi(string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdTransaksiById(string IdTransaction, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PreviewFile(string IdHdTransaksi, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListItemBarangBukti(string NoPerkara, string accessToken, CancellationToken cancellationToken);
    }
}
