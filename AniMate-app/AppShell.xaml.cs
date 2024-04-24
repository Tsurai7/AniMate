using AniMate_app.Views;

namespace AniMate_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("mainpage", typeof(MainPage));
            Routing.RegisterRoute("serchpage", typeof(SearchPage));
            Routing.RegisterRoute("registrationpage", typeof(SignUpPage));
            Routing.RegisterRoute("playerpage", typeof(PlayerPage));
            Routing.RegisterRoute("titlepage", typeof(TitlePage));
            Routing.RegisterRoute("genrepage", typeof(GenrePage));
            Routing.RegisterRoute("profilepage", typeof(ProfilePage));
            Routing.RegisterRoute("loginpage", typeof(SignInPage));
            Routing.RegisterRoute("updatespage", typeof(UpdatesPage));
        }
    }
}