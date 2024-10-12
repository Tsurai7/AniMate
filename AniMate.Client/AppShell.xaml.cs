using AniMate_app.Views;

namespace AniMate_app;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("SearchPage", typeof(SearchPage));
        Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
        Routing.RegisterRoute("PlayerPage", typeof(PlayerPage));
        Routing.RegisterRoute("TitlePage", typeof(TitlePage));
        Routing.RegisterRoute("GenrePage", typeof(GenrePage));
        Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
        Routing.RegisterRoute("SignInPage", typeof(SignInPage));
        Routing.RegisterRoute("UpdatesPage", typeof(UpdatesPage));
    }
}
