using DentalApplication.Common;
using DentalApplication.ServicesController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.ServicesController.GetById
{
    public class GetServiceByIdCommand : CommandBase, IRequest<ServiceById>
    {
        [FromRoute(Name = "id")]
        public Guid? service_id { get; set; }
    }
}
