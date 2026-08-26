using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceFlow.Models
{
    [Table("gastos")]
    public class Gasto
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("concepto")]
        public string Concepto { get; set; } = string.Empty;

        [Column("fecha", TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column("monto", TypeName = "numeric(12, 2)")]
        public decimal Monto { get; set; }

        [Column("categoria_id")]
        public long CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }

        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }
    }
}
