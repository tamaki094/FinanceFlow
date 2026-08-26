using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceFlow.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("fecha_registro")]
        public DateTime? FechaRegistro { get; set; }

        [MaxLength(255)]
        [Column("foto_url")]
        public string? FotoUrl { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("email_verificado")]
        public bool? EmailVerificado { get; set; }

        [Column("estatus_activo")]
        public bool? EstatusActivo { get; set; }

        [MaxLength(255)]
        [Column("proveedor")]
        public string? Proveedor { get; set; }

        [MaxLength(255)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        [Column("uid")]
        public string? Uid { get; set; }
    
    }
}
