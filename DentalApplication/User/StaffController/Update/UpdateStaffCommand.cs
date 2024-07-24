using DentalApplication.Common;
using DentalDomain.Users.Enums;
using MediatR;

namespace DentalApplication.User.StaffController.Update
{
    public class UpdateStaffCommand : CommandBase, IRequest<StaffResponse>
    {
        public string new_email { get; set; }
        public string new_first_name { get; set; }
        public string new_last_name { get; set; }
        public string new_username { get; set; }
        public string new_password { get; set; }
        public string new_phone { get; set; }
        public Role new_role { get; set; }
        public DateTime new_birthday { get; set; }
    }
}
