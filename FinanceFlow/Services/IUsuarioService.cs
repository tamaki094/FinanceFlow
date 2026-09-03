using FinanceFlow.Dtos;
using FinanceFlow.Models;

namespace FinanceFlow.Services
{
    public interface IUsuarioService
    {
        public bool ActualizarUsuario(UsuarioRequestDto usuarioDto);
        public UsuarioResponseDto BuscarUsuario(string  usuarioId);
    }
}
