using DentalApplication.Common;
using DentalApplication.ServicesController.DTO;
using MediatR;

namespace DentalApplication.ServicesController.GetAllForDoctors
{
    public class GetAllForDoctorsCommand : CommandBase, IRequest<List<ListService>>
    {
    }
}
