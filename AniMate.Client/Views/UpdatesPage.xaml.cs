using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class UpdatesPage : ContentPage
{
    private UpdatesViewModel _viewModel;

    private bool _isFirstLoad = true;
    private bool _isOpeningPlayer;

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

            randomButton.IsVisible = true;
        }

        _isFirstLoad = false;
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_viewModel.IsBusy)
            return;

        var collection = sender as CollectionView;

        if (_isOpeningPlayer)
        {
            collection.SelectedItem = null;

            return;
        }

        _isOpeningPlayer = true;

        var navigationParameter = new Dictionary<string, object>
        {
            {"TheTitle", collection.SelectedItem}
        };

        await Shell.Current.GoToAsync($"TitlePage", navigationParameter);

        collection.SelectedItem = null;

        _isOpeningPlayer = false;
    }

    private async void RandomTitleButtonClicked(object sender, EventArgs e)
    {
        if(_viewModel.IsBusy) return;

        _viewModel.IsBusy = true;

        var tokenSource = new CancellationTokenSource();

        var token = tokenSource.Token;

        Task.Factory.StartNew(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                await randomButton.RelRotateTo(360, 400, Easing.SinIn);

                await Task.Delay(100);
            }
        }, token);

        var title = await _viewModel.GetRandomTitle();

        await tokenSource.CancelAsync();

        tokenSource.Dispose();

        _viewModel.IsBusy = false;

        var navigationParameter = new Dictionary<string, object>
        {
            {"TheTitle", title}
        };

        await Shell.Current.GoToAsync($"TitlePage", navigationParameter);
    }
}