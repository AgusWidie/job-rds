using ApiBarangBukti.Help;
using ApiBarangBukti.Models;

namespace ApiBarangBukti.Repository.IRepository
{
    public interface IHdBarangBukti
    {
        Task<GlobalObjectResponse> AddHdBarangBukti(HdBarangBukti parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateHdBarangBukti(HdBarangBukti parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdBarangBukti(CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataHdBarangBuktiById(string IdHdBarangBukti, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> GetPreviewFile(string IdHdBarangBukti, CancellationToken cancellationToken);
    }
}
