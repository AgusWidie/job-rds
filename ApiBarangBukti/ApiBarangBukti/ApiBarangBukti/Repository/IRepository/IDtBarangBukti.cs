using ApiBarangBukti.Help;
using ApiBarangBukti.Models;

namespace ApiBarangBukti.Repository.IRepository
{
    public interface IDtBarangBukti
    {
        Task<GlobalObjectResponse> AddDtBarangBukti(DtBarangBukti parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateDtBarangBukti(DtBarangBukti parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataDtBarangBukti(string IdHdBarangBukti, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> GetPreviewFile(string IdDtBarangBukti, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListItemDtBarangBukti(string NoPerkara, CancellationToken cancellationToken);
    }
}
