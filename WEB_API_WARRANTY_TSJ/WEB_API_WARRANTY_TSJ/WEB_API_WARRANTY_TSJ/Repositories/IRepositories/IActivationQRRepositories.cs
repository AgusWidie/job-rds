using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models.QRCode;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface IActivationQRRepositories
    {
        Task<GlobalObjectResponse> AddActivationQR(ActivationQrRequest parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataActionCodeQR(string? ActionCode, bool? SelectDate, DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken);
    }
}
