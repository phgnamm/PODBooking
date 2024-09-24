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
using Repositories.Repositories;
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //Rating
            services.AddScoped<IRatingRepository, RatingRepository>();  
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRatingCommentRepository, RatingCommentRepository>();
            //Pod
            services.AddScoped<IPodRepository, PodRepository>();
            services.AddScoped<IPodService, PodService>();

            return services;
        }
    }
}
