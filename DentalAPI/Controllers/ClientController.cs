using DentalApplication.Errors;
using DentalContracts.UserContracts.ClientContracts;
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
        [HttpPost]
        public async Task<string> AddClient(AddClientCommand command)
        {
            await _mediator.Send(command);
            return "sdfsdfdsf";
        }
        [HttpGet("success")]
        public IActionResult GetSuccessResponse()
        {
            var data = new { Id = 1, Name = "Sample Data" }; // Replace with actual data
            return RestResponseMapper.Map("success", StatusCodes.Status200OK, data, "Request was successful.");
        }
        [HttpGet("error")]
        public IActionResult GetErrorResponse()
        {
            var errorDetails = new { ErrorCode = "123", ErrorDescription = "An error occurred" }; // Replace with actual error details
            return RestResponseMapper.Map("error", StatusCodes.Status400BadRequest, errorDetails, "An error occurred while processing your request.");
        }

    }
}
