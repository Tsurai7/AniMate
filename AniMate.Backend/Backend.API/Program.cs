using System.Text.Json;
using Backend.API;
using Backend.API.Hubs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services.AddHealthChecks().AddCheck("Memory Usage", () =>
{
    var memoryUsed = GC.GetTotalMemory(false);
    return memoryUsed < 500_000_000
        ? HealthCheckResult.Healthy($"Memory usage: {memoryUsed / 1024 / 1024} MB")
        : HealthCheckResult.Degraded($"High memory usage: {memoryUsed / 1024 / 1024} MB");
});

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation();
        metrics.AddHttpClientInstrumentation();
        metrics.AddRuntimeInstrumentation();
        metrics.AddOtlpExporter();
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapHub<SharedWatchingHub>("/sharedWatchingHub");
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e =>
                new { key = e.Key, status = e.Value.Status.ToString() })
        });
        await context.Response.WriteAsync(result);
    }
});

app.UseMetricServer();
app.UseStaticFiles();
app.Run();