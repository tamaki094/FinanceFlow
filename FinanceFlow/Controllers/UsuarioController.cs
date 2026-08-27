using FinanceFlow.Dtos;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service, ILogger<UsuarioController> logger)
        {
            this._service = service;
            this._logger = logger;
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

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado");
            }
        }
    }
}
