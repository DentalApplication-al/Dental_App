using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalDomain.Users.Enums;
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
            var requestMakerRole = context
                .User
                .Claims
                .FirstOrDefault(x => x.Type == CustomClaim.Role)?.Value;

            if (string.IsNullOrEmpty(requestMakerRole))
            {
                throw new NotAuthorizedException(_localizer.Get(Error.NOT_AUTHORIZED));
            }

            //Try to parse the role claim value to the Role enum
            if (Enum.TryParse<Role>(requestMakerRole, out var role) && Enum.IsDefined(typeof(Role), role))
            {
                // Get permissions for the role
                var permissions = JwtTokenGenerator.GetPermissionsForRole(role);

                // Check if the required permission is present or if the role is ADMIN
                if (permissions.Contains(requirement.Permission) || permissions.Contains("ADMIN"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new NotAuthorizedException(_localizer.Get(Error.NOT_AUTHORIZED));

                }
            }
            else
            {
                throw new NotAuthorizedException(_localizer.Get(Error.NOT_AUTHORIZED));
            }

            return Task.CompletedTask;
        }
    }
}
