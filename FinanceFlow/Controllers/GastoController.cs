using Azure;
using Azure.Core;
using FinanceFlow.Dtos;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastoController : ControllerBase
    {
        private readonly ILogger<GastoController> _logger;
        private readonly IGastoService _gastoService;
        private readonly IUsuarioService _usuarioService;


        public GastoController(IGastoService gastoService, ILogger<GastoController> logger, IUsuarioService usuarioService)
        {
            this._gastoService = gastoService;
            this._logger = logger;
            this._usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<HATEOASresponse<List<GastoResponseDto>>>> ConsultarGastosPorUsuario([FromQuery] string usuarioId)
        {
            var usuario = _usuarioService.BuscarUsuario(usuarioId);

            if (usuario == null || !usuario.id.HasValue)
            {
                return NotFound($"No se encontro el usuario con el UID {usuarioId} ");
            }

            var gastos = _gastoService.ConsultarGastosPorUsuario(usuario.id.Value);

            var links = new List<LinkDto>
                {             
                    new LinkDto
                    {
                        Rel = "consultar",
                        Href = Url.Action(nameof(ConsultarGastosPorUsuario), "Gasto", new { usuarioId = usuarioId })!,
                        Method = "GET"
                    }
                };

            var hateoasResponse = new HATEOASresponse<List<GastoResponseDto>>(gastos, links);

            return Ok(hateoasResponse);
        }


        [HttpPost]
        public async Task<ActionResult<HATEOASresponse<GastoResponseDto>>> CrearGasto([FromBody] GastoRequestDto request)
        {
            try
            {
                var usuario = _usuarioService.BuscarUsuario(request.usuario);

                if (usuario == null || !usuario.id.HasValue)
                {
                    return NotFound($"No se encontro el usuario con el UID {request.usuario} ");
                }


                GastoResponseDto response = _gastoService.RegistrarGasto(request, usuario.id.Value);


                var links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Rel = "self",
                        Href = $"{Url.Action(nameof(CrearGasto), "Gasto")!}/{response.idGasto}",
                        Method = "POST"
                    },
                    new LinkDto
                    {
                        Rel = "consultar",
                        Href = Url.Action(nameof(ConsultarGastosPorUsuario), "Gasto", new { uid = request.usuario })!,
                        Method = "GET"
                    },
                    //new LinkDto
                    //{
                    //    Rel = "delete",
                    //    Href = Url.Action(nameof(EliminarGasto), "Gasto", new { id = response.Id })!,
                    //    Method = "DELETE"
                    //},
                    //new LinkDto
                    //{
                    //    Rel = "update",
                    //    Href = Url.Action(nameof(ActualizarGasto), "Gasto", new { id = response.Id })!,
                    //    Method = "PUT"
                    //}
                };

                var hateoasResponse = new HATEOASresponse<GastoResponseDto>(response, links);

                _logger.LogInformation("Petición HTTP POST recibida en /api/gasto");

                return CreatedAtAction(
                    nameof(CrearGasto),
                    new { id = response.idGasto },
                    hateoasResponse
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error en  en /api/gasto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado");
            }        
        }
    }
}
