using DentalDomain.Services;

namespace DentalDomain.Users.Staffs
{
    public class StaffServices
    {
        public Guid StaffId { get; set; }
        public Guid ServiceId { get; set; }
        public Staff Staff { get; set; }
        public Service Service { get; set; }
    }
}
