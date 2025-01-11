using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalApplication.Extensions;
using DentalApplication.User.StaffController.DTO;
using DentalContracts.AuthenticationContracts;
using MediatR;

namespace DentalApplication.User.StaffController.ChangeMyPassword
{
    public class ChangeMyPasswordCommandHandler : IRequestHandler<ChangeMyPasswordCommand, AuthenticationResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IBlobStorage _blobStorage;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserTokenService _userTokenService;
        public ChangeMyPasswordCommandHandler(
            IStaffRepository staffRepository,
            IBlobStorage blobStorage,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserTokenService userTokenService)
        {
            _staffRepository = staffRepository;
            _blobStorage = blobStorage;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userTokenService = userTokenService;
        }

        public async Task<AuthenticationResponse> Handle(ChangeMyPasswordCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetById(request.logged_in_staff_id.Value, request.clinic_id.Value) ??
                throw new NotFoundException("The user could not be found.");

            if (staff.Password != request.oldPassword)
            {
                throw new BadRequestException("The old password is incorrect.");
            }
            else
            {
                staff.ChangePassword(request.newPassword);
                await _staffRepository.UpdateAsync(staff);

                await _staffRepository.SaveChangesAsync().EnsureSaved();

                var token = _jwtTokenGenerator.GenerateToken(staff.Id, staff.Role, staff.ClinicId);
                var authResponse = new AuthenticationResponse { token = token };
                authResponse.staff = StaffResponse.Map(staff);
                authResponse.staff.picture = _blobStorage.GetLink(staff.ProfilePic ?? "");
                await _userTokenService.MakeTokenInvalidAsync(staff.Id);
                await _userTokenService.AddTokenAsync(staff.Id, token);
                return authResponse;
            }
        }
    }
}
