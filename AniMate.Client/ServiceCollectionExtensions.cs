using AniMate_app.Clients;
using AniMate_app.Interfaces;
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
       
        var accountUrl = string.Empty;
#if DEBUG
        accountUrl = "http://10.0.2.2:5002/api/";
#else
        accountUrl = "http://192.168.105.95:5002/api/";
#endif
        
        services.AddSingleton<IAccountClient>(sp =>
        {
            var client = new AccountClient(sp.GetRequiredService<IHttpClientFactory>());
            return client;
        });
        services.AddHttpClient(nameof(AccountClient),
            client => client.BaseAddress = new Uri(accountUrl));
        
        services.AddSingleton<SharedWatchingClient>();

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