using Backend.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var connectionString = "your-mongodb-connection-string";
            return new MongoClient(connectionString);
        });
        
        
        return services;
    }
}