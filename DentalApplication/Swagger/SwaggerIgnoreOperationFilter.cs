using DentalApplication.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DentalApplication.Swagger
{
    public class SwaggerIgnoreOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Ensure operation has parameters
            if (operation.Parameters == null)
                return;

            // Get the type of the command used in the operation
            var commandType = context.MethodInfo.GetParameters()
                .Select(p => p.ParameterType)
                .FirstOrDefault(t => typeof(CommandBase).IsAssignableFrom(t));

            if (commandType != null)
            {
                var ignoredProperties = commandType.GetProperties()
                    .Where(prop => prop.GetCustomAttributes(true).OfType<SwaggerIgnoreAttribute>().Any())
                    .Select(prop => prop.Name)
                    .ToList();

                // Remove the ignored properties from operation parameters
                operation.Parameters = operation.Parameters
                    .Where(p => !ignoredProperties.Contains(p.Name))
                    .ToList();
            }
        }
    }

}
