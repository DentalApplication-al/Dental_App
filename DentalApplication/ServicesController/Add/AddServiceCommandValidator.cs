using FluentValidation;

namespace DentalApplication.ServicesController.Add
{
    public class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
            RuleFor(a => a.name).Must(a => !string.IsNullOrEmpty(a)).WithMessage("Name should have a value.");
            RuleFor(a => a.price).Must(a => a != null && a > 0).WithMessage("Price should have a value.");
            RuleFor(a => a.duration).Must(a => a != null && a > 0).WithMessage("Duration must have a value.");
        }
    }
}
