using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBarangBukti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HdTransaksiController : ControllerBase
    {
        private readonly ILogger<HdTransaksiController> _logger;
        private readonly IHdTransaksi _hdTransService;

        public HdTransaksiController(ILogger<HdTransaksiController> logger, IHdTransaksi hdTransService)
        {
            _logger = logger;
            _hdTransService = hdTransService;
        }

        [Route("AddHdTransaksi")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> AddHdTransaksi([FromBody] HdTransaksi param, CancellationToken cancellationToken = default)
        {
            var result = await _hdTransService.AddHdTransaksi(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("UpdateHdTransaksi")]
        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> UpdateHdTransaksi([FromBody] HdTransaksi param, CancellationToken cancellationToken = default)
        {
            var result = await _hdTransService.UpdateHdTransaksi(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataHdTransaksi")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataHdTransksi(CancellationToken cancellationToken = default)
        {
            var result = await _hdTransService.ListDataHdTransaksi(cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("ListDataHdTransaksiById")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> ListDataHdTransaksiById(string IdTransaction, CancellationToken cancellationToken = default)
        {
            var result = await _hdTransService.ListDataHdTransaksiById(IdTransaction, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("GetPreviewFile")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetPreviewFile(string IdHdTransaksi, CancellationToken cancellationToken = default)
        {
            var result = await _hdTransService.GetPreviewFile(IdHdTransaksi, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }
            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
