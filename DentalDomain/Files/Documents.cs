using DentalDomain.Appointments;
using DentalDomain.Users.Clients;
using System.Runtime.InteropServices;

namespace DentalDomain.Files
{
    public class Documents : BaseEntity
    {
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public string? Name { get; set; }
        public int? Size { get; set; }
        public string Extension { get; set; }
        public string? Unit { get; set; }
        public bool IsImage { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        
        //private Documents(){}

    }
}
