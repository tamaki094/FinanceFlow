using FinanceFlow.Dtos;
using FinanceFlow.Models;

namespace FinanceFlow.Mappers
{
    public class GastoMapper
    {
        public static Gasto toGastoModel(GastoRequestDto gastoDto, long idUsuario = 0) {
            return new Gasto
            {
                CategoriaId = gastoDto.tipo_gasto,
                Monto = gastoDto.monto,
                UsuarioId = idUsuario,
                Concepto = gastoDto.name,
                Fecha = gastoDto.fecha_creacion
            };
        }

        public static GastoResponseDto toGastoDto(Gasto gasto, CategoriaDto? categoria = null)
        {
            return new GastoResponseDto( 
                idGasto: gasto.Id,
                categoria_gasto : categoria != null ? categoria!.Descripcion! :gasto.Categoria.Descripcion,
                fecha_creacion : gasto.Fecha,
                monto : gasto.Monto,
                name : gasto.Concepto,
                tipo_gasto :  gasto.CategoriaId,
                usuario : gasto.Usuario!.Uid!,
                fecha_actualizacion : null,
                fecha_vencimiento : null,
                fecha_recordatorio : null
            );
        }

        public static List<GastoResponseDto> toListGastoDto(List<Gasto> gastos)
        {
            List<GastoResponseDto> listGastos = new List<GastoResponseDto>();
            
            foreach (var gasto in gastos)
            {
                listGastos.Add(GastoMapper.toGastoDto(gasto));          
            }

            return listGastos;
        }
    }
}
