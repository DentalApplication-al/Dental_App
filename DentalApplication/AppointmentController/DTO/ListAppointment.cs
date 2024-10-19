namespace DentalApplication.AppointmentController.DTO
{
    public class ListAppointment
    {
        public Guid id { get; set; }
        public string? client { get; set; }
        public string? doctors { get; set; }
        public string? treatment { get; set; }
        public string date { get; set; }
        public string time { get; set; }
    }
}
