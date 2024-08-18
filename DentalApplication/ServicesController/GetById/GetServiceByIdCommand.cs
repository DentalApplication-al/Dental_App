using DentalApplication.Common;
using MediatR;

namespace DentalApplication.ServicesController.GetById
{
    public class GetServiceByIdCommand : CommandBase, IRequest<ServiceResponse>
    {
        public Guid? service_id { get; set; }
    }
}
