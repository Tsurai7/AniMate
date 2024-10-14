using Backend.AnilibriaWorker;
using DotNetEnv;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
        
var settings = MongoClientSettings.FromConnectionString(connectionString);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);

builder.Services.AddSingleton<IMongoClient, MongoClient>(_ =>
    new MongoClient(settings));

builder.Services.AddSingleton<AnimeRepository>(sp =>
    new AnimeRepository(
        sp.GetRequiredService<IMongoClient>(),
        "AniMate", 
        "Titles"
    ));
        
builder.Services.AddSingleton<AnilibriaClient>();
        
builder.Services.AddHttpClient(nameof(AnilibriaClient), client =>
{
    client.BaseAddress = new Uri("https://api.anilibria.tv/v3/");
});

var host = builder.Build();
host.Run();