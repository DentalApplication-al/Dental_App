using DentalContracts.AuthenticationContracts;
using FluentValidation;

namespace DentalApplication.Authencation
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            //    RuleFor(a => a.password).MinimumLength(8);
            //    RuleFor(a => a.username).MinimumLength(8);
        }
    }
}
