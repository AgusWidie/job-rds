using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrinterController : ControllerBase
    {
        private readonly ILogger<PrinterController> _logger;
        private readonly IPrinterRepositories _printerService;

        public PrinterController(ILogger<PrinterController> logger, IPrinterRepositories printerService)
        {
            _logger = logger;
            _printerService = printerService;
        }

        [Route("GetPrinterName")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetPrinterName(string? PrinterValue, CancellationToken cancellationToken = default)
        {
            var result = await _printerService.GetDataPrinterName(PrinterValue, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
