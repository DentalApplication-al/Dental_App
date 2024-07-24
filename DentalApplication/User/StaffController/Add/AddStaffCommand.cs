using DentalApplication.Common;
using DentalDomain.Users.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace DentalApplication.User.StaffController.Add
{
    public class AddStaffCommand : CommandBase, IRequest<StaffResponse>
    {
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? phone { get; set; }
        public Role? role { get; set; }
        public DateTime? birthday { get; set; }
    }
}
