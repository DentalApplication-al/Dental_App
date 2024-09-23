using FluentValidation;

namespace DentalApplication.User.ClientController.AddClientFile
{
    public class AddClientFileCommandValidator : AbstractValidator<AddClientFileCommand>
    {
        public AddClientFileCommandValidator()
        {
            RuleFor(a => a.files).Must(a => a != null && a.All(b => b.Length < 5_242_880))
                .WithMessage("Files must not be null and have a maximum of 5 mb.");
        }
    }
}
