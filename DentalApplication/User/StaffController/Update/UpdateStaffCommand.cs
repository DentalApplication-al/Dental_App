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
        public Guid? staff_id { get; set; }
        public string? new_email { get; set; }
        public string? new_first_name { get; set; }
        public string? new_last_name { get; set; }
        public string? new_phone { get; set; }
        public Role? new_role { get; set; }
        public DateOnly? new_birthday { get; set; }
        public string? new_job_type { get; set; }
        public IFormFile? new_picture { get; set; }
        public string? new_start_time { get; set; }
        public string? new_end_time { get; set; }
        public List<Guid>? new_services { get; set; }
    }
}
