using MediatR;

namespace DentalContracts.AuthenticationContracts
{
    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
