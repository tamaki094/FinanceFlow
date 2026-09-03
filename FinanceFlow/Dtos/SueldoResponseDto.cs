namespace FinanceFlow.Dtos
{
    public record SueldoResponseDto
    (
        decimal Sueldo,
        DateTime FechaCreacion,
        string Usuario,
        DateTime? FechaActualizacion
    );
    
}
