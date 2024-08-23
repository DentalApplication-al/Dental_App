using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.Delete
{
    public class DeleteStaffCommand : IRequest
    {
        [FromRoute(Name = "id")]
        public Guid staff_id { get; set; }
    }
}
