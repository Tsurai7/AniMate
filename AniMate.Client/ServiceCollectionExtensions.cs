using AniMate_app.Clients;
using AniMate_app.Interfaces;
using AniMate_app.Services;
using AniMate_app.ViewModels;
using AniMate_app.Views;

namespace AniMate_app;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IAnimeClient>(sp =>
        {
            var client = new AnimeClient(sp.GetRequiredService<IHttpClientFactory>());
            return client;
        });
        services.AddHttpClient(nameof(AnimeClient),
            client => client.BaseAddress = new Uri("https://api.anilibria.tv/v3/"));
       
        var animateBackendUrl = string.Empty;
#if DEBUG
        animateBackendUrl = "http://10.0.2.2:5002/api/";
#else
        animateBackendUrl = "https://tsurai7-animate-910d.twc1.net/api/";
#endif
        
        services.AddSingleton<AccountClient>(sp =>
        {
            var client = new AccountClient(sp.GetRequiredService<IHttpClientFactory>());
            return client;
        });
        services.AddHttpClient(nameof(AccountClient),
            client => client.BaseAddress = new Uri(animateBackendUrl));
        
        services.AddSingleton<AuthClient>(sp =>
        {
            var client = new AuthClient(sp.GetRequiredService<IHttpClientFactory>());
            return client;
        });
        services.AddHttpClient(nameof(AccountClient),
            client => client.BaseAddress = new Uri(animateBackendUrl));
        
        services.AddSingleton<SharedWatchingClient>();

        services.AddSingleton<IApplicationLinkService, ApplicationLinkService>();

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

#if ANDROID
        services.AddSingleton<IScreenOrientationService, AniMate_app.AndroidScreenOrientationService>();
#endif
//#elif IOS
        //services.AddSingleton<IFullScreenService, YourAppNamespace.Platforms.iOS.FullScreenService>();

        services.AddTransient<PlayerViewModel>();
        services.AddTransient<PlayerPage>();

        services.AddTransient<ProfileViewModel>();
        services.AddTransient<ProfilePage>();
        
        services.AddTransient<EditProfileViewModel>();
        services.AddTransient<EditProfilePage>();
            
        services.AddTransient<SignInViewModel>();
        services.AddTransient<SignInPage>();
            
        services.AddTransient<SignUpViewModel>();
        services.AddTransient<SignUpPage>();

        services.AddTransient<SharedWatchingViewModel>();
        services.AddTransient<SharedWatchingPage>();
        
        return services;
    }
}