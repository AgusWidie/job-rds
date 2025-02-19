using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBarangBukti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DtTransaksiController : ControllerBase
    {
        private readonly ILogger<DtTransaksiController> _logger;
        private readonly IDtTransaksi _dtTransService;

        public DtTransaksiController(ILogger<DtTransaksiController> logger, IDtTransaksi dtTransService)
        {
            _logger = logger;
            _dtTransService = dtTransService;
        }

        [Route("AddDtTransaksi")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddDtTransaksi([FromBody] DtTransaksi param, CancellationToken cancellationToken = default)
        {
            var result = await _dtTransService.AddDtTransaksi(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateDtTransaksi")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateDtTransaksi([FromBody] DtTransaksi param, CancellationToken cancellationToken = default)
        {
            var result = await _dtTransService.UpdateDtTransaksi(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataDtTransaksi")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataDtTransaksi(string IdTransaksi, string NoPerkara, CancellationToken cancellationToken = default)
        {
            var result = await _dtTransService.ListDataDtTransaksi(IdTransaksi, NoPerkara, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetPreviewFile")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetPreviewFile(int Id, CancellationToken cancellationToken = default)
        {
            var result = await _dtTransService.GetPreviewFile(Id, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
