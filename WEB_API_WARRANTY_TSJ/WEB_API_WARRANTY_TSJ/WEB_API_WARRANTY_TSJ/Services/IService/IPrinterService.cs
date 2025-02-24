using System.Threading.Tasks;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;

namespace WEB_API_WARRANTY_TSJ.Services.IService
{
    public interface IPrinterService
    {
        Task<GlobalObjectResponse> PrintBarcodeSerialQR(BarcodeSerialQr parameter, string PrinterName, string Source, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PrintActivationQR(ActivationQr parameter, string PrinterName, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> PrintBarcodeSerialQRExist(string? SerialCode, string? RegistrationCode, DateTime? CreatedAt, string PrinterName, string Source, CancellationToken cancellationToken);
    }
}
