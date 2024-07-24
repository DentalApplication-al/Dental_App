using DentalApplication.User.SuperAdmin;
using DentalApplication.User.SuperAdmin.AddClinic;
using DentalApplication.User.SuperAdmin.Login;
using DentalContracts.AuthenticationContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuperAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<AuthenticationResponse> SuperadminLogin(LoginSuperAdminCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        [HttpPost("add-clinic")]
        public async Task<ClinicResponse> AddClinic(AddClinicCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        private string GetTokenFromRequest()
        {
            var token = Request.Headers["Authorization"].ToString();
            if (token.StartsWith("bearer") || token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            return token;
        }
    }
}
