using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Models.Warranty;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TemplatePrintController : ControllerBase
    {
        private readonly ILogger<TemplatePrintController> _logger;
        private readonly ITemplatePrintRepositories _templatePrintService;

        public TemplatePrintController(ILogger<TemplatePrintController> logger, ITemplatePrintRepositories templatePrintService)
        {
            _logger = logger;
            _templatePrintService = templatePrintService;
        }

        [Route("AddTemplatePrint")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddTemplatePrint([FromBody] TemplatePrint param, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.AddTemplatePrint(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateTemplatePrint")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateTemplatePrint([FromBody] TemplatePrint param, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.UpdateTemplatePrint(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateDefaultTemplatePrint")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateDefaultTemplatePrint([FromBody] TemplatePrint param, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.UpdateDefaultTemplatePrint(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("DeleteTemplatePrint")]
        [HttpDelete]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> DeleteTemplatePrint(string? TemplateName, string? Source, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.DeleteTemplatePrint(TemplateName, Source, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListGetDataTemplatePrint")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListGetDataTemplatePrint(string? TemplateName, string? Source, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.ListDataTemplatePrint(TemplateName, Source, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetDataDefaultTemplatePrint")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetDataDefaultTemplatePrint(string? Source, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.GetDataDefaultTemplatePrint(Source, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetDefaultTemplatePrint")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetDefaultTemplatePrint(string? Source, CancellationToken cancellationToken = default)
        {
            var result = await _templatePrintService.DataDefaultTemplatePrint(Source, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
