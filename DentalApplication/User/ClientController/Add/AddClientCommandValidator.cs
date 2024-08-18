using DentalContracts.UserContracts.ClientContracts;
using FluentValidation;

namespace DentalApplication.User.ClientController.Add
{
    public class AddClientCommandValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientCommandValidator()
        {
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
