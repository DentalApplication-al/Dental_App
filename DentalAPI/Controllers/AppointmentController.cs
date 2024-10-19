using DentalApplication.AppointmentController.Add;
using DentalApplication.AppointmentController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> AddAppointment(AddAppointmentRequest request)
        {
            var command = new AddAppointmentCommand(request);
            command = Token.GetClinicId(HttpContext, command);
            return await _mediator.Send(command);
        }
    }
}
