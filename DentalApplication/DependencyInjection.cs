using DentalApplication.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace DentalApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(
               typeof(IPipelineBehavior<,>),
               typeof(ValidationBehavior<,>));

            services.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config) // Read settings from appsettings.json
                .Enrich.FromLogContext()
                .WriteTo.Console() // Optional: Log to console for debugging
                .CreateLogger();


            return services;
        }

    }
}
