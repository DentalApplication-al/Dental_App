using DentalApplication.Common;
using DentalContracts.AuthenticationContracts;
using MediatR;

namespace DentalApplication.User.StaffController.ChangeMyPassword
{
    public class ChangeMyPasswordCommand : CommandBase, IRequest<AuthenticationResponse>
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
