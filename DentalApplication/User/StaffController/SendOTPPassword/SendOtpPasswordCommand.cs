using MediatR;

namespace DentalApplication.User.StaffController.SendOTPPassword
{
    public class SendOtpPasswordCommand : IRequest<bool>
    {
        public string email { get; set; }
    }
}
