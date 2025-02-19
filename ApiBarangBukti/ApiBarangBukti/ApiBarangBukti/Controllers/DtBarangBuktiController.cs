using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBarangBukti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DtBarangBuktiController : ControllerBase
    {
        private readonly ILogger<DtBarangBuktiController> _logger;
        private readonly IDtBarangBukti _dtBarangBuktiService;

        public DtBarangBuktiController(ILogger<DtBarangBuktiController> logger, IDtBarangBukti dtBarangBuktiService)
        {
            _logger = logger;
            _dtBarangBuktiService = dtBarangBuktiService;
        }


        [Route("AddDtBarangBukti")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddDtBarangBukti([FromBody] DtBarangBukti param, CancellationToken cancellationToken = default)
        {
            var result = await _dtBarangBuktiService.AddDtBarangBukti(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateDtBarangBukti")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateDtBarangBukti([FromBody] DtBarangBukti param, CancellationToken cancellationToken = default)
        {
            var result = await _dtBarangBuktiService.UpdateDtBarangBukti(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataDtBarangBukti")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataDtBarangBukti(string IdHdBarangBukti, CancellationToken cancellationToken = default)
        {
            var result = await _dtBarangBuktiService.ListDataDtBarangBukti(IdHdBarangBukti, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetPreviewFile")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetPreviewFile(string IdDtBarangBukti, CancellationToken cancellationToken = default)
        {
            var result = await _dtBarangBuktiService.GetPreviewFile(IdDtBarangBukti, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListItemDtBarangBukti")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListItemDtBarangBukti(string NoPerkara, CancellationToken cancellationToken = default)
        {
            var result = await _dtBarangBuktiService.ListItemDtBarangBukti(NoPerkara, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
