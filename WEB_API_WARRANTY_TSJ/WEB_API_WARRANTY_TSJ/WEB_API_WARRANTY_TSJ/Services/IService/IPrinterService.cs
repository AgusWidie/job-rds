using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;

namespace WEB_API_WARRANTY_TSJ.Services.IService
{
    public interface IPrinterService
    {
        Task<GlobalObjectResponse> PrintBarcodeSerialQR(BarcodeSerialQr parameter, string PrinterName, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PrintActivationQR(ActivationQr parameter, string PrinterName, CancellationToken cancellationToken);
    }
}
