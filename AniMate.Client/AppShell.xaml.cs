using AniMate_app.Views;
using System;

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
        Routing.RegisterRoute("EditProfilePage", typeof(EditProfilePage));
        Routing.RegisterRoute("SignInPage", typeof(SignInPage));
        Routing.RegisterRoute("UpdatesPage", typeof(UpdatesPage));
        Routing.RegisterRoute("SharedWatchingPage", typeof(SharedWatchingPage));
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
