using FinanceFlow.Dtos;

namespace FinanceFlow.Services
{
    public interface IGastoService
    {
        public GastoResponseDto RegistrarGasto(GastoRequestDto gastoDto);
    }
}
