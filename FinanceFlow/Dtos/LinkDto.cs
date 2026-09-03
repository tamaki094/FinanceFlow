namespace FinanceFlow.Dtos
{
    public class LinkDto
    {
        public string Rel { get; set; }   // relación: self, insert, update, delete
        public string Href { get; set; }  // URL
        public string Method { get; set; } // verbo HTTP
    }

}
