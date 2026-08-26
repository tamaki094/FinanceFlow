using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceFlow.Models
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(150)]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("es_gasto")]
        public bool EsGasto { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
    }
}
