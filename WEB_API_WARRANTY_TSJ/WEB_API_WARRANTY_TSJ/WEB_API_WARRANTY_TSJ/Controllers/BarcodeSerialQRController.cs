using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BarcodeSerialQRController : ControllerBase
    {

        private readonly ILogger<BarcodeSerialQRController> _logger;
        private readonly IBarcodeSerialQRRepositories _barcodeSerialQRService;
        private readonly IPrinterService _printService;

        public BarcodeSerialQRController(ILogger<BarcodeSerialQRController> logger, IBarcodeSerialQRRepositories barcodeSerialQRService, IPrinterService printService)
        {
            _logger = logger;
            _barcodeSerialQRService = barcodeSerialQRService;
            _printService = printService;
        }


        [Route("AddBarcodeSerialQRTSJ")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddBarcodeSerialQRTSJ([FromBody] BarcodeSerialQr param, CancellationToken cancellationToken = default)
        {
            var result = await _barcodeSerialQRService.AddBarcodeSerialQRTSJ(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("AddBarcodeSerialQRInoac")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddBarcodeSerialQRInoac([FromBody] BarcodeSerialQr param, CancellationToken cancellationToken = default)
        {
            var result = await _barcodeSerialQRService.AddBarcodeSerialQRInoac(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("DeleteBarcodeSerialQR")]
        [HttpDelete]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> DeleteBarcodeSerialQR(string? SerialCode, string? Source, CancellationToken cancellationToken = default)
        {
            var result = await _barcodeSerialQRService.DeleteSerialCode(SerialCode, Source, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataBarcodeSerialQR")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataBarcodeSerialQR(string? SerialCode, string? Source, bool? SelectDate, DateTime? CreatedAtFrom, DateTime? CreatedAtTo, CancellationToken cancellationToken = default)
        {
            var result = await _barcodeSerialQRService.ListDataBarcodeSerialQR(SerialCode, Source, SelectDate, CreatedAtFrom, CreatedAtTo, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("RePrintSerialNumberQR")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> RePrintSerialNumberQR(string? SerialCode, string? RegistrationCode, CancellationToken cancellationToken = default)
        {
            var result = await _barcodeSerialQRService.RePrintSerialNumberQR(SerialCode, RegistrationCode, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
