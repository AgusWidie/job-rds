using WebBarangBukti.Help;
using WebBarangBukti.Models;

namespace WebBarangBukti.Service.IService
{
    public interface IHdBarangBuktiService
    {
        Task<GlobalObjectResponse> AddHdBarangBukti(HdBarangBukti parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateHdBarangBukti(HdBarangBukti parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdBarangBukti(string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdBarangBuktiById(string IdHdBarangBukti, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PreviewFile(string IdHdBarangBukti, string accessToken, CancellationToken cancellationToken);
    }
}
