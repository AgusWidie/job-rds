using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Models.QRCode;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface IBarcodeSerialQRRepositories
    {
        Task<GlobalObjectResponse> AddBarcodeSerialQRTSJ(BarcodeSerialQr parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> AddBarcodeSerialQRInoac(BarcodeSerialQr parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> DeleteSerialCode(string SerialCode, string? Source, CancellationToken cancellationToken);
        Task<GlobalObjectListResponse> ListDataBarcodeSerialQR(string? SerialCode, string? Source, bool? SelectDate, DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken);
    }
}
