using DentalApplication.Errors;
using Microsoft.Extensions.Localization;

namespace DentalApplication.Resources
{
    public static class StringLocalizerExtensions
    {
        public static string Get(this IStringLocalizer localizer, Error error, params object[] args)
        {
            var localizedString = localizer[error.ToString()];

            // If no arguments are provided, return the localized string without formatting
            if (args == null || args.Length == 0)
            {
                return localizedString;
            }
            return string.Format(localizedString.Value, args);
        }
    }
}
