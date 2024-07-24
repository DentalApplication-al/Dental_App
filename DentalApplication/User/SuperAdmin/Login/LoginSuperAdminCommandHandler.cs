using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalContracts.AuthenticationContracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.SuperAdmin.Login
{
    public class LoginSuperAdminCommandHandler : IRequestHandler<LoginSuperAdminCommand, AuthenticationResponse>
    {
        private readonly ISuperAdminRepository _superAddminRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public LoginSuperAdminCommandHandler(ISuperAdminRepository superAddminRepository, IJwtTokenGenerator jwtTokenGenerator, IStringLocalizer<SharedResource> localizer)
        {
            _superAddminRepository = superAddminRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _localizer = localizer;
        }

        public async Task<AuthenticationResponse> Handle(LoginSuperAdminCommand request, CancellationToken cancellationToken)
        {
            var superAdmin = await _superAddminRepository.GetSuperAdminByUsername(request.username) ??
                throw new NotFoundException("Account not found.");
            if (superAdmin.Password != request.password)
            {
                throw new NotAuthenticatedException(_localizer.Get(Error.WRONG_EMAIL));
            }
            var token = _jwtTokenGenerator.GenerateSuperAdminToken();
            return new AuthenticationResponse()
            {
                Token = token,
            };
        }
    }
}
