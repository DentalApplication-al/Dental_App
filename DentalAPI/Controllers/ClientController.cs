using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Errors;
using DentalApplication.User.ClientController.ClientAppointments;
using DentalApplication.User.ClientController.Delete;
using DentalApplication.User.ClientController.DTO;
using DentalApplication.User.ClientController.GetAll;
using DentalApplication.User.ClientController.GetById;
using DentalApplication.User.ClientController.Update;
using DentalContracts.UserContracts.ClientContracts;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HasPermission(Permission.ADDCLIENT)]
        [HttpPost("add")]
        public async Task<Guid> AddClient(AddClientCommand command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.UPDATECLIENT)]
        [HttpPut("update/{id}")]
        public async Task<Guid> UpdateClient(UpdateClientCommand command, Guid id)
        {
            command.id = id;
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.GETALLCLIENTS)]
        [HttpGet("get-all")]
        public async Task<PaginatedResponse<ListClient>> AllClients([FromQuery] GetAllClientCommand? command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.GETCLIENTBYID)]
        [HttpGet("get-by-id/{id}")]
        public async Task<ClientResponse> GetClientById([FromQuery] GetClientByIdCommand? command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.GETCLIENTAPPOINTMENTS)]
        [HttpGet("getClientAppointments/{id}")]
        public async Task<PaginatedResponse<ListAppointment>> GetClientAppointments([FromQuery] GetClientAppointmentsCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.DELETECLIENT)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteClient([FromQuery] DeleteClientCommand command)
        {
            await _mediator.Send(command);
        }

    }
}
