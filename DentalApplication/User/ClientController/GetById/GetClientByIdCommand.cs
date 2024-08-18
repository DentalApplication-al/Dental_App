using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetById
{
    public class GetClientByIdCommand : CommandBase, IRequest<ClientResponse>
    {
        public Guid? client_id { get; set; }
    }
}
