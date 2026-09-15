using Azure;
using Azure.Core;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;
using FinanceFlow.Models;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SueldoController : ControllerBase
    {

        private readonly ILogger<SueldoController> _logger;
        private readonly ISueldoService _service;
        private readonly IUsuarioService _usuarioService;

        public SueldoController(ILogger<SueldoController> logger, ISueldoService service, IUsuarioService usuarioService)
        {
            this._logger = logger;
            this._service = service;
            this._usuarioService = usuarioService;
        }


        [HttpGet]
        public async Task<ActionResult<HATEOASresponse<List<SueldoPresupuesto>>>> ConsultarSueldoPorId([FromQuery]string usuarioId)
        {
            try
            {
                var usuario = _usuarioService.BuscarUsuario(usuarioId);

                if (usuario == null)
                {
                    return NotFound($"No se encontro el usuario con UID {usuarioId}");
                }

                var sueldos = _service.BuscarSueldoPorId(usuario.id ?? 0);

                var links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Rel = "self",
                        Href = Url.Action(nameof(ConsultarSueldoPorId), new { usuarioId }),
                        Method = "GET"
                    },
                    new LinkDto
                    {
                        Rel = "insert",
                        Href = Url.Action(nameof(ActualizarSueldo)),
                        Method = "POST"
                    }
                };

                    var hateoasResponse = new HATEOASresponse<List<SueldoResponseDto>>(sueldos, links);

                    _logger.LogInformation(
                            LogTemplates.JsonResponse,
                            HttpContext.Request.Path,
                            HttpContext.Request.Method,
                            usuarioId);

                    return Ok(hateoasResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error en {nameof(ConsultarSueldoPorId)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error");
            }
            
        }

        [HttpPost]
        public  async Task<ActionResult<HATEOASresponse<SueldoResponseDto>>> ActualizarSueldo([FromBody]SueldoRequestDto request)
        {
            try
            {
                var usuario = _usuarioService.BuscarUsuario(request.Usuario);
                if(usuario == null)
                {
                    return NotFound($"No se encontro el usuario con UID {request.Usuario}");
                }
  
                var response = _service.ActualizarSueldo(request, usuario.id ?? 0);

                var links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Rel = "self",
                        Href = Url.Action(
                            nameof(ConsultarSueldoPorId),
                            "Sueldo",
                            new { usuarioId = response.Usuario }
                        ),
                        Method = "GET"
                    },
                    new LinkDto
                    {
                        Rel = "create",
                        Href = Url.Action(nameof(ActualizarSueldo), "Sueldo"),
                        Method = "POST"
                    }
                };

                var hateoasResponse = new HATEOASresponse<SueldoResponseDto>(response, links);

                _logger.LogInformation(
                    LogTemplates.JsonResponse, 
                    HttpContext.Request.Path, 
                    HttpContext.Request.Method, 
                    JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true }));

                return CreatedAtAction(
                    nameof(Index),
                    new { id = response.Usuario },
                    hateoasResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error en {nameof(ConsultarSueldoPorId)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error");
            }

        }
    }
}
