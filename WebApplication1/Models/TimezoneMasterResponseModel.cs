namespace WebApplication1.Models
{
    public class TimezoneMasterResponseModel
    {
        public int Code { get; set; }

        public string Messsage { get; set; }

        public int TotalTime { get; set; }

        public TimezoneResponsePaginationModel Data { get; set; }
    }
}
