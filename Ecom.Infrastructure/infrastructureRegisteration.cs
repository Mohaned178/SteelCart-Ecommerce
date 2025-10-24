using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Repositries;
using Ecom.infrastructure.Repositries.Service;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Repositories.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Ecom.infrastructure;

public static class infrastructureRegisteration
{
    public static IServiceCollection infrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddDbContext<AppDbContext>(op =>
        {
            op.UseSqlServer(configuration.GetConnectionString("Shop"));
        });

        
        services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));

        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRating, RatingRepositry>();
        services.AddScoped<IGenerateToken, GenerateToken>();
        services.AddScoped<IPaymentService, PaymentService>();

        
        services.AddSingleton<IConnectionMultiplexer>(i =>
        {
            var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
            return ConnectionMultiplexer.Connect(config);
        });

        services.AddSingleton<IImageManagementService, ImageManagementService>();
        services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
        );

        
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        
        var secretKey = configuration["Token:Secret"];
        var issuer = configuration["Token:Issuer"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT Secret is not configured in appsettings.json");
        }

        if (string.IsNullOrEmpty(issuer))
        {
            throw new InvalidOperationException("JWT Issuer is not configured in appsettings.json");
        }

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = false, 
                                          

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                
                NameClaimType = "name",
                RoleClaimType = "role"
            };

            
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    
                    var authHeader = context.Request.Headers["Authorization"].ToString();

                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Token = authHeader.Substring("Bearer ".Length).Trim();
                    }
                    
                    else if (context.Request.Cookies.ContainsKey("token"))
                    {
                        context.Token = context.Request.Cookies["token"];
                    }

                    return Task.CompletedTask;
                },

                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }

                    
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },

                OnTokenValidated = context =>
                {
                    
                    var email = context.Principal?.FindFirst("email")?.Value
                               ?? context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                    Console.WriteLine($"Token validated for: {email}");
                    return Task.CompletedTask;
                },

                OnChallenge = context =>
                {
                    
                    Console.WriteLine($"OnChallenge error: {context.Error}, {context.ErrorDescription}");
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}