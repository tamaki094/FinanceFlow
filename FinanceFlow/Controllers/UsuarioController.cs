using FinanceFlow.Dtos;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _service;
        private readonly IAuthService _authService;

        public UsuarioController(IUsuarioService service, ILogger<UsuarioController> logger, IAuthService authService)
        {
            this._service = service;
            this._logger = logger;
            this._authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> ActualizarUsuario([FromBody]UsuarioRequestDto usuarioDto)
        {
            try
            {
                var response = _service.ActualizarUsuario(usuarioDto);

                if (response)
                {
                    var token = _authService.GenerarToken(usuarioDto.uid);
                    return StatusCode(StatusCodes.Status201Created, new {usuario = usuarioDto.uid, token = token});
                }

                return NotFound("Usuario no existe");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error en {nameof(ActualizarUsuario)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado");
            }
        }
    }
}
