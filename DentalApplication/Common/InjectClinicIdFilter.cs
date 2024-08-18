using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DentalApplication.Common
{
    public class InjectClinicIdFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = GetTokenFromRequest(context.HttpContext);

            if (!string.IsNullOrEmpty(token))
            {
                var decodedToken = Token.DecodeToken(token);
                var clinicId = decodedToken.clinic != null ? Guid.Parse(decodedToken.clinic) : Guid.Empty;
                var staffid = decodedToken.sub != null ? Guid.Parse(decodedToken.sub) : Guid.Empty;

                foreach (var argument in context.ActionArguments.Values)
                {
                    if (argument is CommandBase command)
                    {
                        command.clinic_id = clinicId;
                        command.loged_in_staff_id = staffid;
                    }
                }
            }

            await next();
        }

        private string GetTokenFromRequest(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            return authorizationHeader.Replace("Bearer ", "");
        }
    }
}
