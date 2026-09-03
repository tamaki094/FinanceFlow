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


        public GastoController(IGastoService gastoService, ILogger<GastoController> logger)
        {
            this._gastoService = gastoService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GastoResponseDto>> CrearGasto([FromBody] GastoRequestDto request)
        {
            try
            {
                var nuevoGasto = request;
                GastoResponseDto response = _gastoService.RegistrarGasto(nuevoGasto);


                _logger.LogInformation("Petición HTTP POST recibida en /api/gasto");
                return CreatedAtAction(
                    nameof(Index),
                    new { id = response.id },
                    response
                );

                //// o tambien...
                //return StatusCode(StatusCodes.Status201Created, response);

                //// o tambien
                //return Created($"/api/gastos/{response.id}", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error en  en /api/gasto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado");
            }
        
            
        }
    }
}
