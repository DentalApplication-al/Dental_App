using DentalApplication.Errors;
using System.IdentityModel.Tokens.Jwt;

public static class CommandMapper
{
    /// <summary>
    /// Maps fields from the source to destination and adds fields from the token if login is required.
    /// </summary>
    public static TCommand MapWithLogin<TRequest, TCommand>(TRequest request, HttpContext context)
        where TCommand : class, new()
        where TRequest : class
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        // Create a new instance of TCommand and map common fields from request to command
        var command = new TCommand();
        MapProperties(request, command);

        // Extract and set fields from the token if login is required
        string token = GetTokenFromRequest(context);
        if (string.IsNullOrEmpty(token))
        {
            throw new NotAuthorizedException("You need to login to access this resource.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken;

        try
        {
            jwtToken = tokenHandler.ReadJwtToken(token);
        }
        catch (Exception ex)
        {
            throw new NotAuthorizedException("You need to login to acces this resource.");
        }

        // Get client ID and clinic ID from the token
        var clientId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var clinicId = jwtToken.Claims.FirstOrDefault(c => c.Type == "clinic")?.Value;

        // Set extracted values into the command object
        var commandType = typeof(TCommand);
        var loggedInStaffIdProperty = commandType.GetProperty("logged_in_staff_id");
        var clinicIdProperty = commandType.GetProperty("clinic_id");

        if (!string.IsNullOrEmpty(clientId) && loggedInStaffIdProperty?.CanWrite == true)
        {
            loggedInStaffIdProperty.SetValue(command, Guid.Parse(clientId));
        }

        if (!string.IsNullOrEmpty(clinicId) && clinicIdProperty?.CanWrite == true)
        {
            clinicIdProperty.SetValue(command, Guid.Parse(clinicId));
        }

        return command;
    }

    /// <summary>
    /// Maps fields from the request to command without requiring login.
    /// </summary>
    public static TCommand MapWithoutLogin<TRequest, TCommand>(TRequest request)
        where TCommand : class, new()
        where TRequest : class
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        // Create a new instance of TCommand and map common fields from request to command
        var command = new TCommand();
        MapProperties(request, command);

        return command;
    }

    /// <summary>
    /// Helper method to map properties with the same name and type from request to command.
    /// </summary>
    private static void MapProperties<TSource, TDestination>(TSource source, TDestination destination)
    {
        var sourceType = typeof(TSource);
        var destinationType = typeof(TDestination);

        foreach (var sourceProperty in sourceType.GetProperties())
        {
            var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
            if (destinationProperty != null && destinationProperty.CanWrite &&
                destinationProperty.PropertyType == sourceProperty.PropertyType)
            {
                var value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, value);
            }
        }
    }

    /// <summary>
    /// Helper method to get the token from the HTTP request.
    /// </summary>
    private static string? GetTokenFromRequest(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return null;
        }

        return authorizationHeader.Replace("Bearer ", "");
    }
}
