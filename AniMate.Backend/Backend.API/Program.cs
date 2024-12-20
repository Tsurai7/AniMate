using Backend.API;
using Backend.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5002");
builder.Services.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.MapHub<SharedWatchingHub>("/sharedWatchingHub");
app.UseStaticFiles();
app.Run();