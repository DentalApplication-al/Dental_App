using DentalApplication.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.ServicesController.Delete
{
    public class DeleteServiceCommand : CommandBase, IRequest<bool>
    {
        [FromRoute(Name = "id")]
        public Guid? service_id { get; set; }
    }
}
