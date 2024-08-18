using FluentValidation;

namespace DentalApplication.ServicesController.Add
{
    public class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
            RuleFor(a => a.name).Must(a => !string.IsNullOrEmpty(a)).WithMessage("Sherbimi duhet te kete nje emer");
            RuleFor(a => a.price).Must(a => a != null && a > 0).WithMessage("Cmimi duhet te kete nje vlere");
            RuleFor(a => a.duration).Must(a => a != null && a > 0).WithMessage("Kohezgjatje duhet te kete nje vlere");
        }
    }
}
