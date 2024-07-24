using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Context;
using DentalInfrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DentalContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ISuperAdminRepository, SuperAddminRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }
    }
}
