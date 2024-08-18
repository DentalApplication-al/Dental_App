using DentalApplication.Common;
using MediatR;

namespace DentalApplication.ServicesController.Get
{
    public class GetAllServicesCommand : CommandBase, IRequest<List<ServiceResponse>>
    {
    }
}
