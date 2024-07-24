using DentalContracts.UserContracts;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.Errors
{
    public class RestResponseMapper
    {
        // Private constructor to hide the implicit public one
        private RestResponseMapper() { }

        public static IActionResult Map(string statusText, int statusCode, object responseObj, string message)
        {
            var map = new Dictionary<string, object>
        {
            { "status", statusText },
            { "data", responseObj },
            { "message", message }
        };

            return new ObjectResult(map) { StatusCode = statusCode };
        }
    }
}
