using FluentValidation;

namespace DentalApplication.ServicesController.Update
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(a => a.new_name).Must(a => !string.IsNullOrEmpty(a)).WithMessage("Sherbimi duhet te kete nje emer");
            RuleFor(a => a.new_price).Must(a => a != null && a > 0).WithMessage("Cmimi duhet te kete nje vlere");
            RuleFor(a => a.new_duration).Must(a => a != null && a > 0).WithMessage("Kohezgjatje duhet te kete nje vlere");
        }
    }
}
