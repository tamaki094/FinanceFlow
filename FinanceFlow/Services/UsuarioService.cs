using FinanceFlow.Data;
using FinanceFlow.Dtos;
using FinanceFlow.Mappers;

namespace FinanceFlow.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool ActualizarUsuario(UsuarioRequestDto usuarioDto)
        {
            //paso inutil...por ahora
            var usuario = _context.Usuarios.FirstOrDefault(w => w.Uid == usuarioDto.uid);

            if (usuario == null)
            {
                usuario = UsuarioMapper.toUsuarioModel(usuarioDto);
                _context.Usuarios.Add(usuario);
            }
            else
            {
                var usuarioActualizado = UsuarioMapper.toUsuarioModel(usuarioDto);
                usuarioActualizado.Id = usuario.Id;

                _context.Entry(usuario).CurrentValues.SetValues(usuarioActualizado);
            }
           
            _context.SaveChanges();

            return true;

        }
    }
}
