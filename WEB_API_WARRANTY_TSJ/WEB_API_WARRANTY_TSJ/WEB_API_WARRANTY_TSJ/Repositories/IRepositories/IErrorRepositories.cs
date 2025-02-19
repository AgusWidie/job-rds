using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface IErrorRepositories
    {
        Task<GlobalObjectResponse> AddLogError(LogError parameter, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataError(DateTime? errorDateFrom, DateTime? errorDateTo, CancellationToken cancellationToken);
    }
}
