using DentalApplication.Common;
using DentalApplication.Swagger;
using DentalDomain.Users.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.ChangeStatus
{
    public class ChangeStatusCommand : CommandBase, IRequest
    {
        [SwaggerIgnore]
        public Guid staff_id { get; set; }
        public StaffStatus status { get; set; }
    }
}
