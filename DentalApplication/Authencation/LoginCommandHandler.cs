using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalContracts.AuthenticationContracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.Authencation
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public LoginCommandHandler(IStaffRepository staffRepository, IJwtTokenGenerator jwtTokenGenerator, IStringLocalizer<SharedResource> localizer)
        {
            _staffRepository = staffRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _localizer = localizer;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetStaffByEmail(request.email);

            if (staff == null || staff.Password != request.password)
            {
                throw new NotAuthorizedException(_localizer.Get(Error.WRONG_EMAIL));
            }
            else
            {
                var token = _jwtTokenGenerator.GenerateToken(staff.Id, staff.Role, staff.ClinicId);
                var authResponse = new AuthenticationResponse { Token = token };
                return authResponse;
            }
        }


    }
}
