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
    }
}
