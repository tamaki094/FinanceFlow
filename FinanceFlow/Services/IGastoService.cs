using FinanceFlow.Dtos;

namespace FinanceFlow.Services
{
    public interface IGastoService
    {
        public GastoResponseDto RegistrarGasto(GastoRequestDto gastoDto, long idUsuario);
        public List<GastoResponseDto> ConsultarGastosPorUsuario(long idUsuario); //verificar como usar filtros aqui, ejemplo: que este mismo emtodo te de opcion de buscar por categoria o por todas las categorias
    }
}
