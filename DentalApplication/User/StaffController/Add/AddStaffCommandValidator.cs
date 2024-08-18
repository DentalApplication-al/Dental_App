using DentalApplication.Errors;
using DentalApplication.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Add
{
    public class AddStaffCommandValidator : AbstractValidator<AddStaffCommand>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public AddStaffCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            RuleFor(a => (int)a.role).InclusiveBetween(3, 5).WithMessage(_localizer.Get(Error.WRONG_ROLE));
            RuleFor(a => a.email).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Email"]));
            RuleFor(a => a.first_name).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["FirstName"]));
            RuleFor(a => a.last_name).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["LastName"]));
            RuleFor(a => a.phone).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Phone"]));
            RuleFor(a => a.birthday).Must(a => a.HasValue).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Birthday"]));
        }

    }
}
