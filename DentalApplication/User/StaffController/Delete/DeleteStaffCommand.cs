using DentalApplication.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.Delete
{
    public class DeleteStaffCommand : CommandBase, IRequest
    {
        [FromRoute(Name = "id")]
        public Guid staff_id { get; set; }
    }
}
