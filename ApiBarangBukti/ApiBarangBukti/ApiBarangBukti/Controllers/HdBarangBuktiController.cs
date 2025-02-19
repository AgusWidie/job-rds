using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBarangBukti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HdBarangBuktiController : ControllerBase
    {
        private readonly ILogger<HdBarangBuktiController> _logger;
        private readonly IHdBarangBukti _hdBarangBuktiService;

        public HdBarangBuktiController(ILogger<HdBarangBuktiController> logger, IHdBarangBukti hdBarangBuktiService)
        {
            _logger = logger;
            _hdBarangBuktiService = hdBarangBuktiService;
        }

        [Route("AddHdBarangBukti")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddHdBarangBukti([FromBody] HdBarangBukti param, CancellationToken cancellationToken = default)
        {
            var result = await _hdBarangBuktiService.AddHdBarangBukti(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateHdBarangBukti")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateHdBarangBukti([FromBody] HdBarangBukti param, CancellationToken cancellationToken = default)
        {
            var result = await _hdBarangBuktiService.UpdateHdBarangBukti(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataHdBarangBukti")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataHdBarangBukti(CancellationToken cancellationToken = default)
        {
            var result = await _hdBarangBuktiService.ListDataHdBarangBukti(cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataHdBarangBuktiById")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataHdBarangBuktiById(string IdHdBarangBukti, CancellationToken cancellationToken = default)
        {
            var result = await _hdBarangBuktiService.ListDataHdBarangBuktiById(IdHdBarangBukti, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetPreviewFile")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetPreviewFile(string IdHdBarangBukti, CancellationToken cancellationToken = default)
        {
            var result = await _hdBarangBuktiService.GetPreviewFile(IdHdBarangBukti, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
