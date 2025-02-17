using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    private readonly GenreViewModel _viewModel;

    private bool _isOpeningPlayer = false;

    public GenrePage(GenreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (_isOpeningPlayer)
        {
            collectionView.SelectedItem = null;

            return;
        }

        _isOpeningPlayer = true;

        if (collectionView.SelectedItem != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collectionView.SelectedItem}
            };

            await Shell.Current.GoToAsync($"TitlePage", navigationParameter);

            collectionView.SelectedItem = null;

            _isOpeningPlayer = false;
        }
    }

    private async void OnAppearing(object sender, EventArgs e)
    {
        await _viewModel.LoadMoreContent();
    }
}