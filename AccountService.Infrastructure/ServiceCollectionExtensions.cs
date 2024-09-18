using AccountService.Infrastructure.Data;
using AccountService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DbConnection")));
        
        services.AddScoped<UserRepository>();

        return services;
    }
}