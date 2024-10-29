using Backend.Application;
using Microsoft.OpenApi.Models;

namespace Backend.API;

public static class StartUp
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSignalR();
        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddControllers().AddNewtonsoftJson();;

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Animate", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your token in the text input below."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }
}
