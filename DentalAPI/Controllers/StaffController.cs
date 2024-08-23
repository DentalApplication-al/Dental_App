using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Resources;
using DentalApplication.User.StaffController;
using DentalApplication.User.StaffController.Add;
using DentalApplication.User.StaffController.ChangePasswordOTP;
using DentalApplication.User.StaffController.Delete;
using DentalApplication.User.StaffController.GetAll;
using DentalApplication.User.StaffController.GetById;
using DentalApplication.User.StaffController.SendOTPPassword;
using DentalApplication.User.StaffController.Update;
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
        private readonly IBlobStorage blobStorage;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMediator _mediator;
        public StaffController(IMediator mediator, IStringLocalizer<SharedResource> stringLocalizer, IBlobStorage blobStorage)
        {
            _mediator = mediator;
            _stringLocalizer = stringLocalizer;
            this.blobStorage = blobStorage;
        }

        [HasPermission(Permission.ADDSTAFF)]
        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<StaffResponse> AddStaff([FromForm] AddStaffCommand command,CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
            //return new StaffResponse();
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponse> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet("roles")]
        public Dictionary<int, string> GetRoles()
        {
            Dictionary<int, string> roles = new();
            var rolesEnum = Enum.GetValues(typeof(Role));
            foreach (var role in rolesEnum)
            {
                roles.Add((int)role, role.ToString().ToUpper());
            }

            roles.Remove(roles.FirstOrDefault(a => a.Value == "ADMIN").Key);
            return roles;
        }

        [HasPermission(Permission.UPDATESTAFF)]
        [HttpPut("update")]
        public async Task<StaffResponse> UpdateStaff(UpdateStaffCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETALLSTAFF)]
        [HttpGet("getall")]
        public async Task<PaginatedResponse<ListStaff>> GetClinicStaff([FromQuery] GetClinicStaffCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken); 
        }

        [HasPermission(Permission.GETSTAFFBYID)]
        [HttpGet("get-by-id/{id}")]
        public async Task<StaffResponse> GetStaffById([FromQuery] GetStaffByIdCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.DELETESTAF)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteStaff([FromQuery] DeleteStaffCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }


        [HttpPost("send-otp")]
        public async Task<bool> SendOtp(SendOtpPasswordCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("change-otp-password")]
        public async Task<bool> ChangeOTPPassword(ChangePasswordOTPCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
