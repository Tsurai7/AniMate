using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;
using static AniMate_app.ViewModels.PlayerViewModel;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel _viewModel;

	public PlayerPage(PlayerViewModel viewModel)
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, true);
        Shell.SetTabBarIsVisible(this, false);
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.ToFullScreen();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        PauseVideo();

        _viewModel.RestoreOrientation();
    }

    private void PauseVideo()
    {
        MediaControl.Stop();
        MediaControl.Handler?.DisconnectHandler();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}