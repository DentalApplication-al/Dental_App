using DentalApplication.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DentalApplication.Swagger
{
    public class SwaggerIgnoreSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (typeof(CommandBase).IsAssignableFrom(context.Type))
            {
                RemoveIgnoredProperties(schema, context.Type);
            }
        }

        private void RemoveIgnoredProperties(OpenApiSchema schema, Type type)
        {
            var ignoredProperties = type.GetProperties()
                .Where(prop => prop.GetCustomAttributes(true).OfType<SwaggerIgnoreAttribute>().Any())
                .Select(prop => prop.Name)
                .ToList();

            foreach (var prop in ignoredProperties)
            {
                schema.Properties.Remove(prop);
            }

            // Handle properties in base classes
            var baseType = type.BaseType;
            while (baseType != null && typeof(CommandBase).IsAssignableFrom(baseType))
            {
                ignoredProperties = baseType.GetProperties()
                    .Where(prop => prop.GetCustomAttributes(true).OfType<SwaggerIgnoreAttribute>().Any())
                    .Select(prop => prop.Name)
                    .ToList();

                foreach (var prop in ignoredProperties)
                {
                    schema.Properties.Remove(prop);
                }

                baseType = baseType.BaseType;
            }
        }
    }

}
