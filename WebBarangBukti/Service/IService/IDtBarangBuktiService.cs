using WebBarangBukti.Help;
using WebBarangBukti.Models;

namespace WebBarangBukti.Service.IService
{
    public interface IDtBarangBuktiService
    {
        Task<GlobalObjectResponse> AddDtBarangBukti(DtBarangBukti parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateDtBarangBukti(DtBarangBukti parameter, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataDtBarangBukti(string IdHdBarangBukti, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PreviewFile(string IdDtBarangBukti, string accessToken, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListItemDtBarangBukti(string NoPerkara, string accessToken, CancellationToken cancellationToken);
    }
}
