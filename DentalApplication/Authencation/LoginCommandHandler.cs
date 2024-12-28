using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalApplication.User.StaffController.DTO;
using DentalContracts.AuthenticationContracts;
using DentalDomain.Users.Enums;
using MediatR;
using Microsoft.Extensions.Localization;
using Serilog;

namespace DentalApplication.Authencation
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IBlobStorage _blob;
        private readonly IUserTokenService _userTokenService;
        public LoginCommandHandler(
            IStaffRepository staffRepository, 
            IJwtTokenGenerator jwtTokenGenerator, 
            IStringLocalizer<SharedResource> localizer, 
            IBlobStorage blob,
            IUserTokenService userTokenService)
        {
            _staffRepository = staffRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _localizer = localizer;
            _blob = blob;
            _userTokenService = userTokenService;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Log.Information("this is first try for serilog.");
            Log.Fatal("Fatal");
            var staff = await _staffRepository.GetStaffByEmail(request.email);

            if (staff == null || staff.Password != request.password)
            {
                throw new NotAuthorizedException(_localizer.Get(Error.WRONG_EMAIL));
            }
            else if (staff.Status == StaffStatus.PASSIVE)
            {
                throw new NotAuthorizedException("Your account has been deactivated");
            }
            else
            {
                var token = _jwtTokenGenerator.GenerateToken(staff.Id, staff.Role, staff.ClinicId);
                var authResponse = new AuthenticationResponse { token = token };
                authResponse.staff = StaffResponse.Map(staff);
                authResponse.staff.picture = _blob.GetLink(staff.ProfilePic ?? "");
                await _userTokenService.AddTokenAsync(staff.Id, token);
                return authResponse;
            }
        }


    }
}
