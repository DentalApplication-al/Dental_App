using FluentValidation;

namespace DentalApplication.ServicesController.Update
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(a => a.name).Must(a => !string.IsNullOrEmpty(a)).WithMessage("Name should have a value.");
            RuleFor(a => a.price).Must(a => a != null && a > 0).WithMessage("Price should have a value.");
            RuleFor(a => a.duration).Must(a => a != null && a > 0).WithMessage("Duration should have a value.");
        }
    }
}
