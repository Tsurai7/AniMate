using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel viewModel;

	public PlayerPage()
	{
		InitializeComponent();

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
    }
}