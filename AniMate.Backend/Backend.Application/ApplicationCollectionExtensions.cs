using Backend.Application.Handlers.Account;
using Backend.Application.Handlers.Title;
using Backend.Application.Services;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application;

public static class ApplicationCollectionExtensions
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
            
            cfg.RegisterServicesFromAssembly(typeof(GetRandomTitleHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetTitlesUpdatesHandler).Assembly);
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
            });
        
        
        return services;
    }
}