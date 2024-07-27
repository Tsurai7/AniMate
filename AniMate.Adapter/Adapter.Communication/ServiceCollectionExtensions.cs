using Adapter.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Adapter.Communication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommunication(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var client = new AnimeClient(
                provider.GetRequiredService<IHttpClientFactory>(),
                provider.GetRequiredService<ILogger<AnimeClient>>());
            return client;
        });

        services
            .AddHttpClient(nameof(AnimeClient), 
                client => client.BaseAddress = new Uri(AppSettings.AnimeProviderUrl));

        services.AddHealthChecks()
            .AddTcpHealthCheck(x => 
                    x.AddHost(new Uri(AppSettings.AnimeProviderUrl).Host, 8080),
                "tcp_health_check",
                HealthStatus.Degraded,
                new[] { "Provider" },
                TimeSpan.FromMinutes(30)
            );

        return services;
    }
}