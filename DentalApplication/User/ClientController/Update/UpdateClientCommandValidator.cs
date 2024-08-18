using FluentValidation;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.client_id)
            .NotEmpty().WithMessage("Client ID is required.");

            RuleFor(x => x.new_first_name)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.new_last_name)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.new_email)
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.new_phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.new_birthday)
               .NotEmpty().WithMessage("Birthday is required.");
        }
    }
}
