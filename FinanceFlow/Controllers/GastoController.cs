using FinanceFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GastoResponseDto>> CrearGasto([FromBody] GastoRequestDto request)
        {
            var nuevoGasto = request;

            return new GastoResponseDto("ENTRETENIMIENTO", DateTime.Now, 100, "ps plus", 1, "tamaki", DateTime.Now);
        }
    }
}
