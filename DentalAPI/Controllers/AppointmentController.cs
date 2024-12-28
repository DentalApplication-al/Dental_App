using DentalApplication.AppointmentController.Add;
using DentalApplication.AppointmentController.DTO;
using DentalApplication.AppointmentController.GetAll;
using DentalApplication.Common;
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
            var command = CommandMapper.MapWithLogin<AddAppointmentRequest, AddAppointmentCommand>(request, HttpContext);
            return await _mediator.Send(command);
        }
        [HttpGet]
        public async Task<PaginatedResponse<ListAppointment>> GetAll([FromQuery] Props? props)
        {
            var command = new GetAllAppointmentsCommand(props);

            Token.GetClinicId(HttpContext, command);

            return await _mediator.Send(command);
        }

        //[HttpGet]
        //public async Task<PaginatedResponse<ListAppointment>> GetResponse(int take, int page, string search)
        //{
         
        //}
    }
}
