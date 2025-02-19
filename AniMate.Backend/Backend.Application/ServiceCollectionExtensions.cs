using Backend.Application.Handlers.Account;
using Backend.Application.Handlers.Title;
using Backend.Application.Services;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddInfrastructure();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SignUpAccountHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(SignInAccountHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetAccountHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UpdateAccountHandler).Assembly);
            
            cfg.RegisterServicesFromAssembly(typeof(GetTitleHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetTitleListHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(SearchTitlesHandler).Assembly);
        });

        services.AddSingleton<TokenService>();

        services.AddHttpContextAccessor();
        
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            }).AddGoogle(options =>
            {
                options.ClientId = "524044807337-d73ak967utq6llo2jo7oorgi1rv93mvr.apps.googleusercontent.com";
                options.ClientSecret = "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";
            } );
        
        
        return services;
    }
}