namespace DentalApplication.Swagger
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;

    public class SwaggerIgnoreFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            var ignoredProperties = context.MethodInfo
                .GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttributes(true).OfType<SwaggerIgnoreAttribute>().Any()))
                .Select(prop => prop.Name);

            operation.Parameters = operation.Parameters
                .Where(p => !ignoredProperties.Contains(p.Name))
                .ToList();
        }
    }

}
