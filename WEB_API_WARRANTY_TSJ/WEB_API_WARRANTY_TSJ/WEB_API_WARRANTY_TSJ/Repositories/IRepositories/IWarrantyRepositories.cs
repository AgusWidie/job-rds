using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models.Warranty;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface IWarrantyRepositories
    {
        Task<GlobalObjectResponse> AddWarranty(WarrantyRequest parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataWarranty(DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListGetDataWarrantyRegistration(string? registrationCode, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateWarrantyRegistration(LogWarrantyRequest parameter, CancellationToken cancellationToken);
    }
}
