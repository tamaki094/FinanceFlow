namespace FinanceFlow.Dtos
{
    public record GastoResponseDto
    (
        string categoria_gasto,
        DateTime fecha_creacion,
        decimal monto,
        string name,
        int tipo_gasto,
        string usuario,
        DateTime fecha_actualizacion,
        DateTime? fecha_vencimiento = null,
        DateTime? fecha_recordatorio = null
    );
}
