using FinanceFlow.Dtos;
using FinanceFlow.Models;

namespace FinanceFlow.Mappers
{
    public class UsuarioMapper
    {

        public static Usuario toUsuarioModel(UsuarioRequestDto dto)
        {
            long idParsed = 0;
            long.TryParse(dto.id, out idParsed);

            return new Usuario
            {
                Uid = dto.uid,
                Email = "",
                EmailVerificado = dto.email_verificado,
                EstatusActivo =  dto.estatus_activo,
                FechaRegistro = dto.fecha_creacion,
                FotoUrl = dto.foto_url,
                Nombre = dto.nombre,
                Proveedor = dto.proveedor,
                Telefono = dto.telefono, 
                Id = idParsed
            };
        }

        public static UsuarioResponseDto? toUsuarioDTO(Usuario? usuario)
        {  
            if(usuario == null)
            {
                return null;
            }
            return new UsuarioResponseDto(
            id: usuario.Id,
            uid: usuario.Uid ?? "",
            nombre: usuario.Nombre,
            telefono: usuario.Telefono,
            email_verificado: usuario.EmailVerificado ?? false,
            proveedor: usuario.Proveedor ?? "",
            fecha_creacion: usuario.FechaRegistro ?? DateTime.Now,
            ultimo_login: null,
            estatus_activo: usuario.EstatusActivo ?? false,
            fecha_actualizacion: null,
            foto_url: usuario.FotoUrl ?? "");
            
            
        }
    }
}
