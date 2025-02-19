using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface ILogRequestActivationQRRepositories
    {
        Task<GlobalObjectResponse> AddLogRequestActivation(LogRequestActivation parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataActivationQR(DateTime? activationDateFrom, DateTime? activationDateTo, CancellationToken cancellationToken);
    }
}
