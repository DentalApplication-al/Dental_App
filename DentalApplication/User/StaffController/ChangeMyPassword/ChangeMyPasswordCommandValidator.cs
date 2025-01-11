using DentalApplication.Behavior;
using FluentValidation;

namespace DentalApplication.User.StaffController.ChangeMyPassword
{
    public class ChangeMyPasswordCommandValidator : AbstractValidator<ChangeMyPasswordCommand>
    {
        public ChangeMyPasswordCommandValidator()
        {
            RuleFor(x => x.oldPassword).StringMustHaveValue("Old password");
            RuleFor(x => x.newPassword).StringMustHaveValue("New password");
        }
    }
}
