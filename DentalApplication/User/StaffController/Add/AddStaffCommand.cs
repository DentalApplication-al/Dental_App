using DentalApplication.Common;
using DentalDomain.Users.Enums;
using MediatR;

namespace DentalApplication.User.StaffController.Add
{
    public class AddStaffCommand : CommandBase, IRequest<StaffResponse>
    {
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public Role? role { get; set; }
        public DateTime? birthday { get; set; }
        public List<Guid>? services { get; set; }
    }
}
