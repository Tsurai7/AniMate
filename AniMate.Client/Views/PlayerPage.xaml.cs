using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel _viewModel;

	public PlayerPage()
	{
		InitializeComponent();
        
        Shell.SetNavBarIsVisible(this, true);
        Shell.SetTabBarIsVisible(this, false);
        
        BindingContext = _viewModel = new PlayerViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PauseVideo();
    }

    private void PauseVideo()
    {
        MediaControl.Pause();
        MediaControl.Handler?.DisconnectHandler();
    }
}