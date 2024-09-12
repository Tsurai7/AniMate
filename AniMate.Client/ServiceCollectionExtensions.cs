using AniMate_app.Services;
using AniMate_app.ViewModels;
using AniMate_app.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AniMate_app;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
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
            
        services.AddSingleton<SignalRService>();
        services.AddTransient<SharedWatchingPage>();
            
        return services;
    }
}