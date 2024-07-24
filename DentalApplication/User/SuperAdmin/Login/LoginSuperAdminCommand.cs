using DentalContracts.AuthenticationContracts;
using MediatR;

namespace DentalApplication.User.SuperAdmin.Login
{
    public class LoginSuperAdminCommand : IRequest<AuthenticationResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
