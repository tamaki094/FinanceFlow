using FinanceFlow.Dtos;
using FinanceFlow.Models;

namespace FinanceFlow.Mappers
{
    public class SueldoMapper
    {
        public static SueldoHistorico toSueldoModel(SueldoRequestDto dto)
        {
            return new SueldoHistorico
            {
                Monto = dto.Sueldo,
                FechaAsignacion = dto.FechaCreacion,
                Activo = true,                
            };
        }

        public static SueldoResponseDto toDTO(SueldoHistorico dto)
        {
            return new SueldoResponseDto(
                Sueldo: dto.Monto,
                FechaCreacion: dto.FechaAsignacion,
                Usuario : dto.Usuario.Uid ?? "",
                FechaActualizacion: null
            );
        }

        public static List<SueldoResponseDto> toListDTO(List<SueldoHistorico> sueldos)
        {
            List<SueldoResponseDto> sueldosDto = new List<SueldoResponseDto>();

            foreach (SueldoHistorico sueldo in sueldos)
            {
                sueldosDto.Add(SueldoMapper.toDTO(sueldo));
                
            }

            return sueldosDto;
        }
    }
}
