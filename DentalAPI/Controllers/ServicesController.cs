using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.ServicesController.Add;
using DentalApplication.ServicesController.Delete;
using DentalApplication.ServicesController.DTO;
using DentalApplication.ServicesController.Get;
using DentalApplication.ServicesController.GetAllForDoctors;
using DentalApplication.ServicesController.GetById;
using DentalApplication.ServicesController.Update;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        public ServicesController(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [HasPermission(Permission.ADDSERVICE)]
        [HttpPost("add")]
        public async Task<ServiceResponse> AddService(AddServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETALLSERVICES)]
        [HttpGet("getall")]
        public async Task<PaginatedResponse<ListService>> GetAllServices([FromQuery] GetAllServicesCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        //[HasPermission(Permission.GETALLSERVICES)]
        [HttpGet("getallfordoctors")]
        public async Task<List<ListService>> GetAllServicesForDoctors([FromQuery] GetAllForDoctorsCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETSERVICEBYID)]
        [HttpGet("getbyid/{id}")]
        public async Task<ServiceById> GetServiceById([FromQuery] GetServiceByIdCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.UPDATESRVICE)]
        [HttpPut("update")]
        public async Task<ServiceResponse> UpdateService([FromBody]UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.DELETESERVICE)]
        [HttpDelete("delete/{id}")]
        public async Task<bool> DeleteService([FromQuery] DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
