using AniMate_app.Views;

namespace AniMate_app;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
        Routing.RegisterRoute(nameof(TitlePage), typeof(TitlePage));
        Routing.RegisterRoute(nameof(GenrePage), typeof(GenrePage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
        Routing.RegisterRoute(nameof(UpdatesPage), typeof(UpdatesPage));
        Routing.RegisterRoute(nameof(SharedWatchingPage), typeof(SharedWatchingPage));
    }

    private async void JoinRoomButtonClicked(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Enter Code", "Share watch room code");

        if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            return;
        
        var roomId = result?.Substring(result.LastIndexOf('/') + 1);
        
        if (!string.IsNullOrEmpty(roomId))
        {
            await Current.GoToAsync($"SharedWatchingPage?RoomId={roomId}");
        }
    }
}
