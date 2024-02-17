using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel viewModel;

	public PlayerPage(string mediaUrl)
	{
		InitializeComponent();

        BindingContext = viewModel = new PlayerViewModel(mediaUrl);
	}
}