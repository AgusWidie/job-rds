using WEB_API_WARRANTY_TSJ.Help;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface IPrinterRepositories
    {
        Task<GlobalObjectResponse> GetDataPrinterName(string? PrinterValue, CancellationToken cancellationToken);
    }
}
