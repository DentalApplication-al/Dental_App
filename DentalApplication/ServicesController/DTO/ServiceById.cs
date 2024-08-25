using DentalApplication.User.StaffController.DTO;

namespace DentalApplication.ServicesController.DTO
{
    public class ServiceById
    {
        public Guid id { get; set; }
        public decimal? price { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? duration { get; set; }
        public List<ListStaff> doctors { get; set; }
    }
}
