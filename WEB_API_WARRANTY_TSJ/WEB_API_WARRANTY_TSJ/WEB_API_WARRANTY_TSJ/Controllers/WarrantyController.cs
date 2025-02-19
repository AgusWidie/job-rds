using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models.Warranty;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyController : ControllerBase
    {
        private readonly ILogger<WarrantyController> _logger;
        private readonly IWarrantyRepositories _warrantyService;

        public WarrantyController(ILogger<WarrantyController> logger, IWarrantyRepositories warrantyService)
        {
            _logger = logger;
            _warrantyService = warrantyService;
        }

        [Route("AddWarranty")]
        [HttpPost]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<Object>> AddWarranty([FromBody] WarrantyRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _warrantyService.AddWarranty(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataWarranty")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataWarranty(DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken = default)
        {
            var result = await _warrantyService.ListDataWarranty(createdAtFrom, createdAtTo, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListGetDataWarrantyRegistration")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListGetDataWarrantyRegistration(string? registrationCode, CancellationToken cancellationToken = default)
        {
            var result = await _warrantyService.ListGetDataWarrantyRegistration(registrationCode, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateWarrantyRegistration")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateWarrantyRegistration([FromBody] LogWarrantyRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _warrantyService.UpdateWarrantyRegistration(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
