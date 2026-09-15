using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Models;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaService service, ILogger<CategoriaController> logger) 
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<HATEOASresponse<List<CategoriaDto>>>> ConsultarCategorias()
        {
            try
            {
                List<CategoriaDto> categorias = _service.GetCategorias();

                var links = new List<LinkDto>
                {
                   new LinkDto
                   {
                       Rel = "self",
                       Href = Url.Action(
                           nameof(ConsultarCategorias),
                           "Categoria")!,
                       Method = "GET",
                   }
                };

                var hateoasResponse = new HATEOASresponse<List<CategoriaDto>>(categorias, links);

                return Ok(hateoasResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error en {nameof(ConsultarCategorias)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrio un error");
            }         
        }
    }
}
