using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Resources;
using DentalApplication.ServicesController.GetAllForDoctors;
using DentalApplication.User.StaffController.Add;
using DentalApplication.User.StaffController.ChangePasswordOTP;
using DentalApplication.User.StaffController.ChangeStatus;
using DentalApplication.User.StaffController.Delete;
using DentalApplication.User.StaffController.DoctorAppointments;
using DentalApplication.User.StaffController.DTO;
using DentalApplication.User.StaffController.GetAll;
using DentalApplication.User.StaffController.GetById;
using DentalApplication.User.StaffController.GetClinicDoctors;
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

        [HttpPost("login")]
        public async Task<AuthenticationResponse> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.ADDSTAFF)]
        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<StaffResponse> AddStaff([FromForm] AddStaffCommand command,CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
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

        [HasPermission(Permission.GETDOCTORS)]
        [HttpGet("doctors")]
        public async Task<List<ListStaff>> GetClinicDoctors([FromQuery] GetClinicDoctorsCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETALLSTAFF)]
        [HttpGet("doctorappointments/{id}")]
        public async Task<PaginatedResponse<ListAppointment>> GetDoctorAppointments([FromQuery] GetDoctorAppointmentCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.UPDATESTAFF)]
        [HttpPut("update/{id}")]
        public async Task<StaffResponse> UpdateStaff([FromForm]UpdateStaffCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.STAFF_CHANGE_STATUS)]
        [HttpPatch("change-status/{id}")]
        public async Task ChangeStaffStatus([FromRoute] Guid id, [FromBody] ChangeStatusCommand command, CancellationToken cancellationToken)
        {
            command.staff_id = id; // Assign the id from the route to the command
            await _mediator.Send(command, cancellationToken);
        }


        [HasPermission(Permission.DELETESTAF)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteStaff([FromQuery] DeleteStaffCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }


        //[HttpGet("roles")]
        //public Dictionary<int, string> GetRoles()
        //{
        //    Dictionary<int, string> roles = new();
        //    var rolesEnum = Enum.GetValues(typeof(Role));
        //    foreach (var role in rolesEnum)
        //    {
        //        roles.Add((int)role, role.ToString().ToUpper());
        //    }

        //    roles.Remove(roles.FirstOrDefault(a => a.Value == "ADMIN").Key);
        //    return roles;
        //}
    }
}
