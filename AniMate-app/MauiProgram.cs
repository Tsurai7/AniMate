using AniMate_app.Services.AnilibriaService;
using AniMate_app.View;
using AniMate_app.ViewModel;
using CommunityToolkit.Maui;
using MediaControls;
using Microsoft.Extensions.Logging;


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

            builder.Services.AddTransient<AnilibriaService>().AddTransient<MainPage>().AddTransient<MainViewModel>();
            builder.Services.AddTransient<AnilibriaService>().AddTransient<SearchPage>().AddTransient<SearchViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}