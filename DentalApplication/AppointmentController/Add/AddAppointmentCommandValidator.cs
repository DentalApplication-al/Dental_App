using DentalApplication.Behavior;
using FluentValidation;

namespace DentalApplication.AppointmentController.Add
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandValidator()
        {
            RuleFor(a => a.startTime).MustBeValidTime("Start time");
            RuleFor(a => a.endTime).MustBeValidTime("End time");
            RuleFor(a => a.serviceId).GuidMustHaveValue("Service must be selected.");
            RuleFor(a => a.doctors).Must(a => a != null && a.Count > 0).WithMessage("One or more doctor need to be selected");
            RuleFor(a => a.clinic_id).GuidMustHaveValue("There was a problem identifying the user.");
            RuleFor(a => a.logged_in_staff_id).GuidMustHaveValue("There was a problem identifying the user.");
            
            RuleFor(a => a)
             .Custom((a, context) =>
             {
                 if (a.existingClient == null && a.newClient == null)
                 {
                     context.AddFailure("The client was not selected.");
                 }
                 else if (a.existingClient != null && a.newClient != null)
                 {
                     context.AddFailure("Both 'Existing Client' and 'New Client' cannot have values at the same time.");
                 }
             });
        }
    }
}
