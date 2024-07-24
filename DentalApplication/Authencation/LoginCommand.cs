using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalContracts.AuthenticationContracts
{
    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
