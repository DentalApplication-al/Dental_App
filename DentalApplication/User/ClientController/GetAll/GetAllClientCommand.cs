using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAll
{
    public class GetAllClientCommand : CommandBase, IRequest<List<ClientListResponse>>
    {
    }
}
