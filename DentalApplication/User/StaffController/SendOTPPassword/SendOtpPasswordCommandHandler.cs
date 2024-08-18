using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalApplication.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.SendOTPPassword
{
    public class SendOtpPasswordCommandHandler : IRequestHandler<SendOtpPasswordCommand, bool>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IEmailService _emailService;
        public SendOtpPasswordCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer, IEmailService emailService)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
        }

        public async Task<bool> Handle(SendOtpPasswordCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetStaffByEmail(request.email) ??
                throw new NotFoundException("We can not find any staff with provided email");

            staff.SetOTP();

            var emailResponse = await _emailService.SendOTPEmailAsync(staff.Email, staff.OTP);

            if (emailResponse.WasSent)
            {
                await _staffRepository.UpdateAsync(staff);
                await _staffRepository.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new EmailNotSentException(_stringLocalizer.Get(Error.EMAIL_NOT_SENT));
            }

        }
    }
}
