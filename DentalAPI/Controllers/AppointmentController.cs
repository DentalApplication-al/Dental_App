using DentalApplication.AppointmentController;
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

        //[HttpPost]
        //public async Task<AppointmentResponse> AddAppointment()
    }
}
