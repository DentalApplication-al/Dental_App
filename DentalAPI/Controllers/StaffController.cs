using DentalApplication.Resources;
using DentalApplication.User.StaffController.Add;
using DentalContracts.AuthenticationContracts;
using DentalDomain.Users.Enums;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;


namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMediator _mediator;
        public StaffController(IMediator mediator, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _mediator = mediator;
            _stringLocalizer = stringLocalizer;
        }

        [HasPermission(Permission.GETSTAFF)]
        [HttpPost("add-staff")]
        public async Task AddStaff(AddStaffCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponse> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet("roles")]
        public List<string> GetRoles()
        {
            List<string> roles = new();
            var rolesEnum = Enum.GetValues(typeof(Role));
            foreach (var role in rolesEnum)
            {
                roles.Add(role.ToString().ToUpper());
            }
            return roles;
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
