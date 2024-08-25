using FluentValidation;

namespace DentalApplication.ServicesController.GetById
{
    public class GetServiceByIdCommandValidator : AbstractValidator<GetServiceByIdCommand>
    {
        public GetServiceByIdCommandValidator()
        {
            RuleFor(a => a.service_id)
                .NotEmpty().WithMessage("Select a service.")
                .NotNull().WithMessage("Select a service.");
        }
    }
}
