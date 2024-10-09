using FluentValidation;

namespace DentalApplication.User.ClientController.GetById
{
    public class GetCLientByIdCommandValidator : AbstractValidator<GetClientByIdCommand>
    {
        public GetCLientByIdCommandValidator()
        {
            RuleFor(a => a.ClientId)
                .NotEmpty().WithMessage("Client id is required.");
        }
    }
}
