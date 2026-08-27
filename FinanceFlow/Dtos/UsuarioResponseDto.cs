namespace FinanceFlow.Dtos
{
    public record UsuarioResponseDto
    (
        string? id,
        string uid,
        string nombre,
        string foto_url,
        string? telefono,
        bool email_verificado,
        string proveedor,
        DateTime fecha_creacion,
        DateTime? ultimo_login,
        bool estatus_activo,
        DateTime? fecha_actualizacion
    );
}
