using DentalApplication.Behavior;
using FluentValidation.Results;

namespace DentalApplication.Errors
{
    public class ValidationnException : Exception
    {
        public List<string> Errors { get; }

        public ValidationnException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.Select(e => e.ErrorMessage).ToList();
        }
    }

}
