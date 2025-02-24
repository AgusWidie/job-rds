using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models.QRCode;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationQRController : ControllerBase
    {

        private readonly ILogger<ActivationQRController> _logger;
        private readonly IActivationQRRepositories _activationQRService;

        public ActivationQRController(ILogger<ActivationQRController> logger, IActivationQRRepositories activationQRService)
        {
            _logger = logger;
            _activationQRService = activationQRService;
        }


        [Route("AddActivationQR")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddActivationQR([FromBody] ActivationQrRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _activationQRService.AddActivationQR(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataActivationQR")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataActivationQR(string? ActivationCode, bool? SelectDate, DateTime? CreatedAtFrom, DateTime? CreatedAtTo, CancellationToken cancellationToken = default)
        {
            var result = await _activationQRService.ListDataActionCodeQR(ActivationCode, SelectDate, CreatedAtFrom, CreatedAtTo, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
