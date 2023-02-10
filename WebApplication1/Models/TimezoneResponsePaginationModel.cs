namespace WebApplication1.Models
{
    public class TimezoneResponsePaginationModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int NumberOfRecords { get; set; }
        public int TotalRecords { get; set; }

        public List<TimezoneResponseModel> Content { get; set; }
    }
}
