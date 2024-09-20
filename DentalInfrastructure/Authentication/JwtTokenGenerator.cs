using DentalApplication.Common.Interfaces.IAuthentication;
using DentalDomain.Users.Enums;
using DentalInfrastructure.Authentication.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentalInfrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly SymmetricSecurityKey key = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes("4e77621985efd3acafbf51577164f36c151735f2e024b9730e373ced05744a9d"));
        public string GenerateToken(Guid userId, Role role, Guid clinicId)
        {
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(CustomClaim.Role, role.ToString()),
                new Claim(CustomClaim.Clinic, clinicId.ToString())
            };

            var securityToken = new JwtSecurityToken(

                issuer: "DentalApplication",
                audience: "DentalApplication",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public static List<string> GetPermissionsForRole(Role role)
        {
            List<string> permissions = new();
            if (role.ToString().ToUpper() == "SUPERADMIN")
            {
                permissions.Add("SUPERADMIN");
            }
            else if (role == Role.ADMIN)
            {
                foreach (var item in Enum.GetValues(typeof(Permission)))
                {
                    permissions.Add(item.ToString().ToUpper());
                }
            }
            else if (role == Role.RECEPTIONIST)
            {
                permissions.Add(Permission.ADDCLIENT.ToString().ToUpper());
                permissions.Add(Permission.GETALLCLIENTS.ToString().ToUpper());
                permissions.Add(Permission.UPDATECLIENT.ToString().ToUpper());
                permissions.Add(Permission.DELETECLIENT.ToString().ToUpper());
                permissions.Add(Permission.GETCLIENTAPPOINTMENTS.ToString().ToUpper());

                permissions.Add(Permission.ADDAPPOINTMENT.ToString().ToUpper());
                permissions.Add(Permission.GETALLAPPOINTMENTS.ToString().ToUpper());
                permissions.Add(Permission.UPDATEAPPOINTMENT.ToString().ToUpper());
                permissions.Add(Permission.DELETEAPPOINTMENT.ToString().ToUpper());

                permissions.Add(Permission.GETALLSERVICES.ToString().ToUpper());
                permissions.Add(Permission.GETALLSTAFF.ToString().ToUpper());
            }
            return permissions;
        }

        public string GenerateSuperAdminToken()
        {
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(CustomClaim.Permission, "SUPERADMIN"),
                new(CustomClaim.Permission, "ADMIN"),
                new(CustomClaim.Role, "SUPERADMIN")
            };
            var securityToken = new JwtSecurityToken(

                issuer: "DentalApplication",
                audience: "DentalApplication",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
