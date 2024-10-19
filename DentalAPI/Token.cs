using DentalApplication.Common;
using DentalApplication.Errors;
using System.IdentityModel.Tokens.Jwt;

namespace DentalAPI
{
    public static class Token
    {
        public static T GetToken<T>(HttpContext context) where T : CommandBase, new()
        {
            // Create an instance of the command object
            T command = new T();

            // Get the token from the request
            string token = GetTokenFromRequest(context);
            if (token == null)
            {
                throw new NotAuthorizedException("You need to login to acces this resource.");
            }

            // Parse the token to extract the ClientId and ClinicId (adjust this part based on your token structure)
            // Assume we are using JWT and have methods to decode it
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var clientId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var clinicId = jwtToken.Claims.FirstOrDefault(c => c.Type == "clinic")?.Value;

            // Populate the command object with the values from the token
            if (!string.IsNullOrEmpty(clientId))
            {
                command.loged_in_staff_id = Guid.Parse(clientId);
            }

            if (!string.IsNullOrEmpty(clinicId))
            {
                command.clinic_id = Guid.Parse(clinicId);
            }

            return command;
        }

        public static T GetClinicId<T>(HttpContext context, T command)
        {
            string token = GetTokenFromRequest(context);
            if (token == null)
            {
                throw new NotAuthorizedException("You need to login to acces this resource.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var clientId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var clinicId = jwtToken.Claims.FirstOrDefault(c => c.Type == "clinic")?.Value;

            var commandType = typeof(T);
            var loggedInStaffIdProperty = commandType.GetProperty("loged_in_staff_id");
            var clinicIdProperty = commandType.GetProperty("clinic_id");

            // Populate the command object with the values from the token using reflection
            if (!string.IsNullOrEmpty(clientId) && loggedInStaffIdProperty != null && loggedInStaffIdProperty.CanWrite)
            {
                // Set the loged_in_staff_id property
                loggedInStaffIdProperty.SetValue(command, Guid.Parse(clientId));
            }

            if (!string.IsNullOrEmpty(clinicId) && clinicIdProperty != null && clinicIdProperty.CanWrite)
            {
                // Set the clinic_id property
                clinicIdProperty.SetValue(command, Guid.Parse(clinicId));
            }

            return command;
        }
        

        private static string GetTokenFromRequest(HttpContext context)
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
