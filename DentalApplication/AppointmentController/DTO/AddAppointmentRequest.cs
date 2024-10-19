using DentalContracts.UserContracts.ClientContracts;

namespace DentalApplication.AppointmentController.DTO
{
    public class AddAppointmentRequest
    {
        public Guid? existingClient { get; set; }
        public AddClientCommand? newClient { get; set; }
        public Guid serviceId { get; set; }
        public List<Guid> doctors { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
        public bool isApproved { get; set; }
    }
}
