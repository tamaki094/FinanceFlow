using FinanceFlow.Dtos;
using FinanceFlow.Models;

namespace FinanceFlow.Mappers
{
    public class CategoriaMapper
    {
        public static CategoriaDto toDto(Categoria categoria)
        {
            return new CategoriaDto(
                Id: categoria.Id,
                Descripcion: categoria.Descripcion,
                EsGasto: categoria.EsGasto,
                Nombre: categoria.Nombre
            );
        }

        public static List<CategoriaDto> toListDto(List<Categoria> categorias)
        {
            List<CategoriaDto> dtos = new List<CategoriaDto>();

            foreach (var categoria in categorias)
            {
                dtos.Add(CategoriaMapper.toDto(categoria));
            }

            return dtos;
        }
    }
}
