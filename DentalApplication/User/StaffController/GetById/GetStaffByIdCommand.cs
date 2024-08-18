using DentalApplication.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.GetById
{
    public class GetStaffByIdCommand : CommandBase, IRequest<StaffResponse>
    {
        [FromRoute(Name = "id")]
        public Guid? staff_id { get; set; }
    }
}
