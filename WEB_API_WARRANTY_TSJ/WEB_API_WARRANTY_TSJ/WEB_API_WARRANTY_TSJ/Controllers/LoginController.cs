using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.ModelsDBERP.Login;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepositories _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginRepositories loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [Route("LoginUserWeb")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> LoginUserWeb([FromBody] LoginRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _loginService.LoginUserWeb(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("LoginUserMobile")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> LoginUserMobile([FromBody] LoginRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _loginService.LoginUserMobile(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }

        [Route("RefreshToken")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> RefreshToken([FromBody] RefreshTokenRequest param, CancellationToken cancellationToken = default)
        {
            var result = await _loginService.RefreshToken(param, cancellationToken);
            if (result.Error == true && result.Message.Substring(0, 7) != MessageRepositories.MessageSuccess)
            {
                return BadRequest(ResponseAPI.CreateError(result.Code, result.Message));
            }

            return Ok(ResponseAPI<Object>.Create(result.Message, result.Data));
        }
    }
}
