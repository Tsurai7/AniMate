using AniMate_app.Services.AnilibriaService;
using AniMate_app.Views;
using AniMate_app.ViewModels;
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

            var services = builder.Services;

            ConfigureServices(services);
          
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            // Service configuration
            services.AddHttpClient<AnilibriaService>();

            // Pages configuration
            services.AddTransient<MainPage>();
            services.AddTransient<MainViewModel>();

            services.AddTransient<UpdatesPage>();
            services.AddTransient<UpdatesViewModel>();

            services.AddTransient<SearchPage>();
            services.AddTransient<SearchViewModel>();

            services.AddTransient<GenreViewModel>();
            services.AddTransient<GenrePage>();

            services.AddTransient<TitleViewModel>();
            services.AddTransient<TitlePage>();

            services.AddTransient<PlayerViewModel>();
            services.AddTransient<PlayerPage>();
        }
    }
}