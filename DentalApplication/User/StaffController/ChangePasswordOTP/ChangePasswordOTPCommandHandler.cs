using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using MediatR;

namespace DentalApplication.User.StaffController.ChangePasswordOTP
{
    public class ChangePasswordOTPCommandHandler : IRequestHandler<ChangePasswordOTPCommand, bool>
    {
        private readonly IStaffRepository _staffRepository;

        public ChangePasswordOTPCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<bool> Handle(ChangePasswordOTPCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetStaffByEmail(request.email) ??
                throw new NotFoundException("The staff could not be found");
            var password = request.new_password;

            if (staff.OTP == request.otp)
            {
                staff.ChangePassword(password);
                await _staffRepository.UpdateAsync(staff);
                await _staffRepository.SaveChangesAsync();
                return true;
            }
            throw new BadRequestException("Wrong password.");
        }
    }
}
