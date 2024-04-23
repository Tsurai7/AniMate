using AniMate_app.Services;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class UpdatesPage : ContentPage
{
	private UpdatesViewModel _viewModel;

	private bool _isFirstLoad = true;

    public UpdatesPage(UpdatesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (_isFirstLoad)
		{
            await _viewModel.LoadContent();

            string myLabel = await _viewModel._authService.GetStringFromApi();

            _viewModel.LabelText = myLabel;

            await _viewModel.LoadSavedData();
        }
			

		_isFirstLoad = false;
    }
}