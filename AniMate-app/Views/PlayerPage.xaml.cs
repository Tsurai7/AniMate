using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel viewModel;

	public PlayerPage()
	{
		InitializeComponent();
        AppShell.SetNavBarIsVisible(this, true);
        AppShell.SetTabBarIsVisible(this, false);
        BindingContext = viewModel = new PlayerViewModel();


    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PauseVideo();
    }

    private void PauseVideo()
    {
        mediaControl.Pause();
        mediaControl.Handler?.DisconnectHandler();
    }
}