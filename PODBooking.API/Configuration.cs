using Microsoft.AspNetCore.Identity;
using PODBooking.API.Middlewares;
using PODBooking.API.Utils;
using Repositories.Entities;
using Repositories;
using System.Diagnostics;
using Services.Common;
using Repositories.Interfaces;
using Repositories.Common;
using Services.Interfaces;
using Services.Services;
namespace PODBooking.API
{
    public static class Configuration
    {
        public static IServiceCollection AddAPIConfiguration(this IServiceCollection services)
        {
            // Identity
            services
                .AddIdentity<Account, Role>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(15);
            });

            // Middlewares
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddScoped<AccountStatusMiddleware>();
            services.AddSingleton<Stopwatch>();

            // Common
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, EmailService>();
            // Dependency Injection
            // Account


            return services;
        }
    }
}
