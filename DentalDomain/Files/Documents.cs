using DentalDomain.Appointments;
using DentalDomain.Users.Clients;

namespace DentalDomain.Files
{
    public class Documents
    {
        public Guid Id { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public string? Name { get; set; }
        public int? Size { get; set; }
        public string? Unit { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public Guid ClinicId { get; set; }
    }
}
