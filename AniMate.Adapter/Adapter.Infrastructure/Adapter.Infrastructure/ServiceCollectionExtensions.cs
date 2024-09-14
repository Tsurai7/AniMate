using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Adapter.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var connectionString = "your-mongodb-connection-string";
            return new MongoClient(connectionString);
        });

        // Добавление UserRepository в DI
        services.AddScopedUserRepository>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return new UserRepository(mongoClient, "YourDatabaseName", "Users");
        });
        
        
        return services;
    }
}