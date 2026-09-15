using System.ComponentModel.DataAnnotations;

namespace FinanceFlow.Dtos
{
    public class LinkDto
    {
        [Required]
        public string Rel { get; set; }   // relación: self, insert, update, delete
        [Required]
        public string Href { get; set; }  // URL
        [Required]
        public string Method { get; set; } // verbo HTTP
    }

}
