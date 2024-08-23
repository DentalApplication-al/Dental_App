using DentalApplication.Errors;
using DentalApplication.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Update
{
    public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UpdateStaffCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            RuleFor(a => (int)a.new_role).InclusiveBetween(3, 5).WithMessage(_localizer.Get(Error.WRONG_ROLE));
            RuleFor(a => a.new_email).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Email"]));
            RuleFor(a => a.new_first_name).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["FirstName"]));
            RuleFor(a => a.new_last_name).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["LastName"]));
            RuleFor(a => a.new_phone).Must(a => !string.IsNullOrEmpty(a)).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Phone"]));
            RuleFor(a => a.new_birthday).Must(a => a.HasValue).WithMessage(_localizer.Get(Error.IS_REQUIRED, _localizer["Birthday"]));
            _localizer = localizer;
        }
    }
}
