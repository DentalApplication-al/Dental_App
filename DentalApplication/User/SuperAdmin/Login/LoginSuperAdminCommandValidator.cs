using FluentValidation;

namespace DentalApplication.User.SuperAdmin.Login
{
    public class LoginSuperAdminCommandValidator : AbstractValidator<LoginSuperAdminCommand>
    {
        public LoginSuperAdminCommandValidator()
        {
            RuleFor(a => a.username).NotEmpty().WithMessage("Username must have a value.");
            RuleFor(a => a.password).NotEmpty().WithMessage("Password must have a value.");
        }
    }
}
