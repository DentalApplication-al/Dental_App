using DentalDomain.Users.Enums;

namespace DentalApplication.Common.Interfaces.IAuthentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateSuperAdminToken();
        string GenerateToken(Guid userId, Role role, Guid clinicId);
    }
}
