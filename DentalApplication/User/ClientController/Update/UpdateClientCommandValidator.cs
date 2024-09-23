using FluentValidation;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.id)
            .NotEmpty().WithMessage("Client Id is required.");

            RuleFor(x => x.first_name)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.last_name)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.birthday)
               .NotEmpty().WithMessage("Birthday is required.");
        }
    }
}
