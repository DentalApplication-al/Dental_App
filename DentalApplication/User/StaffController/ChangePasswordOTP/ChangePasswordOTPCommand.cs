using MediatR;

namespace DentalApplication.User.StaffController.ChangePasswordOTP
{
    public class ChangePasswordOTPCommand : IRequest<bool>
    {
        public string? email { get; set; }
        public string? otp { get; set; }
        public string? new_password { get; set; }
    }
}
