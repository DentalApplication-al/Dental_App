using DentalApplication.Common;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.Update
{
    public class UpdateStaffCommand : CommandBase, IRequest<StaffResponse>
    {
        [FromRoute(Name = "id")]
        public Guid? id { get; set; }
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
        public StaffStatus status { get; set; }
        public Gender gender { get; set; }
        public List<Guid>? services { get; set; }
    }
}
