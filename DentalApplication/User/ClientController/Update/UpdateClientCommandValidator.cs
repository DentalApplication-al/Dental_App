using DentalApplication.User.ClientController.Add;
using FluentValidation;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            Include(new AddClientCommandValidator());
            RuleFor(x => x.id)
            .NotNull().WithMessage("Id is required.");
        }
    }
}
