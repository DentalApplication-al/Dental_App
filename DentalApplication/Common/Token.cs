using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace DentalApplication.Common
{
    public class Token
    {
        public static TokenPayload DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                // Print the claims from the token
                var claims = jsonToken.Claims.Select(c => new { c.Type, c.Value });
                foreach (var claim in claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }

                // Optionally, you can deserialize the payload into a specific class
                var payload = jsonToken.Payload.SerializeToJson();
                var decodedPayload = JsonSerializer.Deserialize<TokenPayload>(payload);
                return decodedPayload;
            }
            throw new Exception("Token not found");
        }


        public class TokenPayload
        {
            public string sub { get; set; }
            public string role { get; set; }
            public string[] permission { get; set; }
            public string clinic { get; set; }
            public long exp { get; set; }
            public string iss { get; set; }
            public string aud { get; set; }
        }
    }
}
