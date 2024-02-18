using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class UpdatesPage : ContentPage
{
	private UpdatesViewModel _viewModel;

    public UpdatesPage(UpdatesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		await _viewModel.LoadContent().ConfigureAwait(false);
    }
}