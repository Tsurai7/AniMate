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
        AppShell.SetNavBarIsVisible(this, false);
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

    private async void OnMediaElementTapped(object sender, TappedEventArgs e)
    {
        if(BackButton.IsVisible != true)
        {
            BackButton.IsVisible = true;
        }
        else
        {
            BackButton.IsVisible = false;
        }

        await Task.Delay(5000);
        BackButton.IsVisible = false;
    }

    private async void OnMediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        if (BackButton.IsVisible != true)
        {
            BackButton.IsVisible = true;
        }
        else
        {
            BackButton.IsVisible = false;
        }

        await Task.Delay(5000);
        BackButton.IsVisible = false;
    }
}