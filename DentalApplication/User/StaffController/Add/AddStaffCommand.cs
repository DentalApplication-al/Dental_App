using DentalApplication.Common;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DentalApplication.User.StaffController.Add
{
    public class AddStaffCommand : CommandBase, IRequest<StaffResponse>
    {
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public Role? role { get; set; }
        public DateOnly? birthday { get; set; }
        public string? job_type { get; set; }
        public IFormFile? picture { get; set; }
        public string? start_time { get; set; }
        public string? end_time { get; set; }
        public List<Guid>? services { get; set; }
    }
}
