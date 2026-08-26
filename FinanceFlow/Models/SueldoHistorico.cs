using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceFlow.Models
{
    [Table("sueldo_historico")]
    public class SueldoHistorico
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("fecha_asignacion", TypeName = "date")]
        public DateTime FechaAsignacion { get; set; }

        [Column("monto", TypeName = "numeric(12, 2)")]
        public decimal Monto { get; set; }

        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }

        public SueldoPresupuesto? Presupuesto { get; set; }
    }
}
