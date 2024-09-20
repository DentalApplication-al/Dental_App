using DentalApplication.Common;
using DentalApplication.Swagger;
using DentalApplication.User.ClientController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.ClientController.GetById
{
    public class GetClientByIdCommand : CommandBase, IRequest<ClientResponse>
    {
        [FromRoute(Name = "id")]
        public Guid? id { get; set; }
    }
}
