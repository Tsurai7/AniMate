using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MediaControls;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.View;


namespace AniMate_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .UseMediaControls();

            builder.Services.AddSingleton<AnilibriaService>();
            builder.Services.AddSingleton<ViewModel.MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}