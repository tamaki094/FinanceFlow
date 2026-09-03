using FinanceFlow.Dtos;

namespace FinanceFlow.Services
{
    public interface ISueldoService
    {
        public SueldoResponseDto ActualizarSueldo(SueldoRequestDto request, long idUsuario);
        public List<SueldoResponseDto> BuscarSueldoPorId(long id);
    }
}
