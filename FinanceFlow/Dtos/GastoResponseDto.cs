namespace FinanceFlow.Dtos
{
    public record GastoResponseDto
    (
        long? idGasto,
        string categoria_gasto,
        DateTime fecha_creacion,
        decimal monto,
        string name,
        long tipo_gasto,
        string usuario,
        DateTime? fecha_actualizacion,
        DateTime? fecha_vencimiento = null,
        DateTime? fecha_recordatorio = null
    );
}
