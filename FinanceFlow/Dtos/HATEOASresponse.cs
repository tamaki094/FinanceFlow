namespace FinanceFlow.Dtos
{
    public class HATEOASresponse<T>
    {
        public T Data { get; set; }
        public List<LinkDto> Links { get; set; }

        public HATEOASresponse(T data, List<LinkDto> links)
        {
            Data = data;
            Links = links;
        }
    }

}
