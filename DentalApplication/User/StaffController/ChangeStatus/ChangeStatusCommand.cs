using DentalApplication.Common;
using DentalDomain.Users.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.ChangeStatus
{
    public class ChangeStatusCommand : CommandBase, IRequest
    {
        [FromRoute(Name = "id")]
        public Guid id { get; set; }
        public StaffStatus status { get; set; }
    }
}
