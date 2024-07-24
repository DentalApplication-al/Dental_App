using DentalDomain.Users.Enums;
using DentalDomain.Users.Staffs;

namespace DentalApplication.User.StaffController
{
    public class StaffResponse 
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string? phone { get; set; }
        public string? username { get; set; }
        public Role? role { get; set; }
        public DateTime birthday { get; set; }
        public StaffResponse(Staff staff)
        {
            first_name = staff.FirstName;
            last_name = staff.LastName;
            email = staff.Email;
            phone = staff.Phone;
            username = staff.Username;
            role = staff.Role;
            birthday = staff.Birthday;
        }
    }
}
