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

        var connectionString = "mongodb+srv://tsurai:yCWmLgoVdcwWRodU@cluster0.hielo.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";//Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");


        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("No environment variable MONGO_CONNECTION_STRING");
        }
        
        services.AddSingleton<IMongoClient, MongoClient>(_ =>
            new MongoClient(connectionString));

        services.AddSingleton<AccountRepository>(sp =>
            new AccountRepository(
                sp.GetRequiredService<IMongoClient>(),
                "animate",
                "accounts"
            ));
        
        services.AddSingleton<AnimeRepository>(sp =>
            new AnimeRepository(
                sp.GetRequiredService<IMongoClient>(),
                "animate", 
                "titles"
            ));
        
        //services.AddHostedService<AnimeWorker>();
        services.AddSingleton<AnilibriaClient>();
        services.AddHttpClient(nameof(AnilibriaClient), client =>
        {
            client.BaseAddress = new Uri("https://api.anilibria.tv/v3/");
        });
        
        return services;
    }
}