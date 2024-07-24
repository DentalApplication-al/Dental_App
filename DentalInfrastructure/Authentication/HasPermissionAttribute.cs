using DentalInfrastructure.Authentication.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DentalInfrastructure.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            :base(permission.ToString())
        {
            
        }
    }
}
