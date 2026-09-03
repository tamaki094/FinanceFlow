using System.Text.Json.Serialization;

namespace FinanceFlow.Dtos
{
    public record SueldoRequestDto
    {
        public decimal Sueldo { get; set; }
        [JsonPropertyName("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        [JsonPropertyName("usuario")]
        public string Usuario { get; set; }
        [JsonPropertyName("fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; } 
    }
}
