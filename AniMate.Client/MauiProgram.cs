using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AniMate_app;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        
        builder.Services.ConfigureServices();
      
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}