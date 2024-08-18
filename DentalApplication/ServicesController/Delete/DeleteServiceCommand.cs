using MediatR;

namespace DentalApplication.ServicesController.Delete
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public Guid? service_id { get; set; }
    }
}
