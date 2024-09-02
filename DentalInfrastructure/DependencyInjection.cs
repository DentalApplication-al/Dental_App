using DentalApplication.Common.Interfaces.IAuthentication;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Context;
using DentalInfrastructure.Repositories;
using DentalInfrastructure.Services;
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
                options.UseSqlServer(connectionString));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ISuperAdminRepository, SuperAddminRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IBlobStorage, BlobStorage>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IUserTokenService, UserTokenService>();

            return services;
        }
    }
}
