using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private PlayerViewModel viewModel;

	public PlayerPage(string mediaUrl)
	{
		InitializeComponent();

		viewModel = new PlayerViewModel(mediaUrl);
	}
}