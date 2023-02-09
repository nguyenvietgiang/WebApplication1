namespace WebApplication1.Models
{
    public class TimezoneResponseModelResponsePagination
    {
        public int Code { get; set; }

        public string Messsage { get; set; }

        public int TotalTime { get; set; }

        public TimezoneResponseModelPagination Data { get; set; }
    }
}
