using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Services
{
    public class GastoService : IGastoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriaService _categoriaService;
        public GastoService(ApplicationDbContext context, ICategoriaService categoriaService) 
        {
            this._context = context;
            this._categoriaService = categoriaService;
        }

        public GastoResponseDto RegistrarGasto(GastoRequestDto gastoDto, long idUsuario)
        {
            Gasto gasto = GastoMapper.toGastoModel(gastoDto, idUsuario);
            var categoria = _categoriaService.GetCategorias().Where(w => w.Descripcion!.ToLower() == gastoDto.categoria_gasto).FirstOrDefault();
            
           
            gasto.CategoriaId = categoria!.Id!.Value;
            _context.Gastos.Add(gasto);
            _context.SaveChanges();
            

            return GastoMapper.toGastoDto(gasto, categoria);
        }

        public List<GastoResponseDto> ConsultarGastosPorUsuario(long idUsuario)
        {
            List<Gasto> gastos = _context.Gastos
                .Where(w => w.UsuarioId == idUsuario)
                .Include( g => g.Categoria)
                .Include(g => g.Usuario)
                .ToList();
            List<GastoResponseDto> gastosDto = GastoMapper.toListGastoDto(gastos);

            return gastosDto;

        }
    }
}


