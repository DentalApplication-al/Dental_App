using DentalApplication.Common;
using DentalApplication.Swagger;
using DentalApplication.User.ClientController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.ClientController.GetById
{
    public class GetClientByIdCommand : CommandBase, IRequest<ClientResponse>
    {
        public Guid? ClientId { get; set; }
    }
}
