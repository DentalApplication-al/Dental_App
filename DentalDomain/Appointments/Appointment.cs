using DentalDomain.Files;
using DentalDomain.Services;
using DentalDomain.Users.Clients;
using DentalDomain.Users.Staffs;

namespace DentalDomain.Appointments
{
    public class Appointment : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public List<Staff> Doctor { get; set; } = [];
        public Client Client { get; set; }
        public Service Service { get; set; }
        public List<Documents> Files { get; set; } = [];

        public bool IsDateValid()
        {
            if (StartDate < EndDate)
            {
                return true;
            }
            return false;
        }
    }
}
