using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface ITemplatePrintRepositories
    {
        Task<GlobalObjectResponse> AddTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> UpdateDefaultTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> DeleteTemplatePrint(string TemplateName, string? Source, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataTemplatePrint(string? TemplateName, string? Source, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> DataDefaultTemplatePrint(string? Source, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> GetDataDefaultTemplatePrint(string? Source, CancellationToken cancellationToken);
    }
}
