using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalDomain.Users.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace DentalInfrastructure.Authentication
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IUserTokenService _userTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionAuthorizationHandler(
            IStringLocalizer<SharedResource> localizer, 
            IUserTokenService userTokenService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _localizer = localizer;
            _userTokenService = userTokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                throw new NotAuthenticatedException(_localizer.Get(Error.NOT_LOGEDIN));
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Assuming you have a user ID from somewhere, like from claims or a service
            var userId = context
                .User
                .Claims
                .FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            // Validate the token
            var isValidToken = _userTokenService.ValidateTokenAsync(Guid.Parse(userId), token);

            if (!isValidToken)
            {
                throw new NotAuthorizedException("Your account is in passive mode.");
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
