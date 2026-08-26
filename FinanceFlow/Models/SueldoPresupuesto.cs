using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceFlow.Models
{

    [Table("SueldosPresupuestos")]
    public class SueldoPresupuesto
    {
        [Key]
        [Column("presupuesto_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PresupuestoId { get; set; }

        [Column("usuario_id")]
        public long? UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }

        [Column("monto_necesarios", TypeName = "decimal(10, 2)")]
        public decimal MontoNecesarios { get; set; }

        [Column("monto_play", TypeName = "decimal(10, 2)")]
        public decimal MontoPlay { get; set; }

        [Column("monto_ahorro", TypeName = "decimal(10, 2)")]
        public decimal MontoAhorro { get; set; }

        [Column("monto_provisiones", TypeName = "decimal(10, 2)")]
        public decimal MontoProvisiones { get; set; }

        [Required]
        [MaxLength(7)]
        [Column("mes_anio")]
        public string MesAnio { get; set; } = string.Empty;

        [Column("sueldo_id")]
        public long SueldoId { get; set; }

        [ForeignKey(nameof(SueldoId))]
        public SueldoHistorico? Sueldo { get; set; }
    }
}
