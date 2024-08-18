using DentalApplication.Errors;
using DentalApplication.User.ClientController.DTO;
using DentalApplication.User.ClientController.GetAll;
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
        public async Task<ClientResponse> AddClient(AddClientCommand command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.UPDATECLIENT)]
        [HttpPut("update")]
        public async Task<ClientResponse> UpdateClient(UpdateClientCommand command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.GETALLCLIENTS)]
        [HttpGet("get-all")]
        public async Task<List<ClientListResponse>> AllClients([FromQuery] GetAllClientCommand? command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("error")]
        public IActionResult GetErrorResponse()
        {
            var errorDetails = new { ErrorCode = "123", ErrorDescription = "An error occurred" }; // Replace with actual error details
            return RestResponseMapper.Map("error", StatusCodes.Status400BadRequest, errorDetails, "An error occurred while processing your request.");
        }

    }
}
