using AniMate_app.Services;
using AniMate_app.ViewModels;
using AniMate_app.Views;
using CommunityToolkit.Maui;
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
            .UseMauiCommunityToolkitMediaElement();

            ConfigureServices(builder.Services);
          
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Services configuration
            services.AddHttpClient<AnilibriaService>();
            services.AddHttpClient<AccountService>();

            // Pages configuration
            services.AddTransient<TitlePage>();
            services.AddTransient<TitleViewModel>();

            services.AddTransient<MainPage>();
            services.AddTransient<MainViewModel>();

            services.AddTransient<UpdatesPage>();
            services.AddTransient<UpdatesViewModel>();

            services.AddTransient<SearchPage>();
            services.AddTransient<SearchViewModel>();

            services.AddTransient<GenreViewModel>();
            services.AddTransient<GenrePage>();

            services.AddTransient<PlayerViewModel>();
            services.AddTransient<PlayerPage>();

            services.AddTransient<ProfileViewModel>();
            services.AddTransient<ProfilePage>();
            
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignInPage>();
            
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<SignUpPage>();
        }
    }
}