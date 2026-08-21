namespace FinanceFlow.Dtos
{
    public record GastoRequestDto
    (
        string categoria_gasto,
        DateTime fecha_creacion,
        decimal monto,
        string name,
        int tipo_gasto,
        string usuario,
        DateTime fecha_actualizacion,
        DateTime? fecha_vencimiento,
        DateTime? fecha_recordatorio
    );

}
