using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Errors;
using DentalApplication.User.ClientController.AddClientFile;
using DentalApplication.User.ClientController.ClientAppointments;
using DentalApplication.User.ClientController.Delete;
using DentalApplication.User.ClientController.DeleteClientFile;
using DentalApplication.User.ClientController.DTO;
using DentalApplication.User.ClientController.GetAll;
using DentalApplication.User.ClientController.GetById;
using DentalApplication.User.ClientController.Update;
using DentalContracts.UserContracts.ClientContracts;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

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

        [HasPermission(Permission.CLIENT_ADD)]
        [HttpPost("add")]
        public async Task<Guid> AddClient(AddClientCommand command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_UPLOAD_FILE)]
        [HttpPost("uploadFile{id}")]
        public async Task UploadClientFile([FromRoute] Guid id, [FromForm] List<IFormFile> files, CancellationToken cancellationToken)
        {
            var command = Token.GetToken<AddClientFileCommand>(HttpContext);
            command.files = files;
            command.clientId = id;

            await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.CLIENT_UPDATE)]
        [HttpPut("update/{id}")]
        public async Task<Guid> UpdateClient([FromBody]UpdateClientCommand command, [FromRoute]Guid id)
        {
            command.clientId = id;
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_GET_ALL)]
        [HttpGet("get-all")]
        public async Task<PaginatedResponse<ListClient>> AllClients([FromQuery] GetAllClientCommand? command)
        {
            var test = Token.GetToken<GetAllClientCommand>(HttpContext);
            test.take = command.take;
            test.page = command.page;
           
            return await _mediator.Send(test);
        }

        [HasPermission(Permission.CLIENT_GET_BY_ID)]
        [HttpGet("get-by-id/{id}")]
        public async Task<ClientResponse> GetClientById([FromRoute] Guid id)
        {
            var command = Token.GetToken<GetClientByIdCommand>(HttpContext);
            command.ClientId = id;
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_GET_APPOINTMENTS)]
        [HttpGet("getClientAppointments/{id}")]
        public async Task<PaginatedResponse<ListAppointment>> GetClientAppointments([FromQuery] GetClientAppointmentsCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.CLIENT_DELETE)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteClient([FromQuery] DeleteClientCommand command)
        {
            await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_UPLOAD_FILE)]
        [HttpDelete("deleteFile{id}")]
        public async Task DeleteClientFile([FromRoute] Guid id, [FromBody]List<Guid> files, CancellationToken cancellationToken)
        {
            var command = Token.GetToken<DeleteClientFileCommand>(HttpContext);
            command.files = files;
            command.clientId = id;
            await _mediator.Send(command, cancellationToken);
        }
    }



    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
