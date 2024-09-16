using DentalDomain.Users.Enums;

namespace DentalApplication.User.StaffController.DTO
{
    public class ListStaff
    {
        public Guid? id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public Role? role { get; set; }
        public string? job_type { get; set; }
        public string? working_hours { get; set; }
        public StaffStatus status { get; set; }
    }
}
