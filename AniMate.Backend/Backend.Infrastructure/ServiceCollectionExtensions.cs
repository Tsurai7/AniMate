using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        Env.Load();
        
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
            new MongoClient("MONGO_CONNECTION_STRING"));

        services.AddScoped<IMongoRepository<User>>(sp =>
            new UserRepository(
                sp.GetRequiredService<IMongoClient>(),
                "AniMate", 
                "Accounts"
            ));
        
        return services;
    }
}