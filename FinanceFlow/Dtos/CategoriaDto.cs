namespace FinanceFlow.Dtos
{
    public record CategoriaDto
    (
        long? Id,
        string? Descripcion,
        bool EsGasto,
        string Nombre
    );
}
