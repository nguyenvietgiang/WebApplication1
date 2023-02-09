namespace WebApplication1.Models
{
    public class TimezoneResponseModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Offset { get; set; }

        public bool? IsDTS { get; set; }

        public int? Oder { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid LastModifiedByUserId { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public DateTime CreatedOnDate { get; set; }
    }
}
