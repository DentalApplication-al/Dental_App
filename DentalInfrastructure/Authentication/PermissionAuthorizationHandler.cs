using DentalApplication.Errors;
using DentalApplication.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace DentalInfrastructure.Authentication
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public PermissionAuthorizationHandler(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                throw new NotAuthenticatedException(_localizer.Get(Error.NOT_LOGEDIN));
            }
            HashSet<string> permissions = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaim.Permission)
                .Select(x => x.Value)
                .ToHashSet();

            if (permissions.Contains(requirement.Permission) || permissions.Contains("ADMIN"))
            {
                context.Succeed(requirement);
            }
            else
            {
                throw new NotAuthorizedException(_localizer.Get(Error.NOT_AUTHORIZED)); 
            }
            return Task.CompletedTask;
        }
    }
}
