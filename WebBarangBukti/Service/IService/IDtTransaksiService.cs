using WebBarangBukti.Help;
using WebBarangBukti.Models;

namespace WebBarangBukti.Service.IService
{
    public interface IDtTransaksiService
    {
        Task<GlobalObjectResponse> AddDtTransaksi(DtTransaksi parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateDtTransaksi(DtTransaksi parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataDtTransaksi(string IdTransaksi, string NoPerkara, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PreviewFile(int Id, string accessToken, CancellationToken cancellationToken);
    }
}
