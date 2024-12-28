using FluentValidation;

namespace DentalApplication.Behavior
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidTime<T>(this IRuleBuilder<T, string> ruleBuilder, string field)
        {
            return ruleBuilder.Matches(@"^([01]\d|2[0-3]):([0-5]\d)$")
                              .WithMessage($"{field} has an invalid time format. Use HH:mm.");
        }
        /// <summary>
        /// Rule is for strings not to be null and have a value.
        /// </summary>
        public static IRuleBuilderOptions<T, string> StringMustHaveValue<T>(this IRuleBuilder<T, string> ruleBuilder, string field)
        {
            return ruleBuilder.Must(a => a != null && a != "")
                              .WithMessage($"{field} must have a value.");
        }

        /// <summary>
        /// Rule is for guids not to be null and have a value.
        /// </summary>
        public static IRuleBuilderOptions<T, Guid?> GuidMustHaveValue<T>(this IRuleBuilder<T, Guid?> ruleBuilder, string message)
        {
            return ruleBuilder.Must(a => a != null && a != Guid.Empty)
                              .WithMessage(message);
        }

        /// <summary>
        /// Rule is for guids not to be null and have a value.
        /// </summary>
        public static IRuleBuilderOptions<T, Guid> GuidMustHaveValue<T>(this IRuleBuilder<T, Guid> ruleBuilder, string message)
        {
            return ruleBuilder.Must(a => a != null && a != Guid.Empty)
                              .WithMessage(message);
        }
    }
}
