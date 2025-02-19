using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IErrorRepositories _errorService;

        public ErrorController(ILogger<ErrorController> logger, IErrorRepositories errorService)
        {
            _logger = logger;
            _errorService = errorService;
        }

        [Route("ListDataError")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataError(DateTime? errorDateFrom, DateTime? errorDateTo, CancellationToken cancellationToken = default)
        {
            var result = await _errorService.ListDataError(errorDateFrom, errorDateTo, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
