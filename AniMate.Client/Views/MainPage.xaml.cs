using AniMate_app.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    private bool _isOpeningPlayer = false;

    private bool _isFirstLoad = true;

    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = mainViewModel;
    }

    private async void LoadContent(object sender, EventArgs e)
    {
        if (_isFirstLoad)
        {
            await _viewModel.LoadContent();

            _isFirstLoad = false;
        }    
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
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

    private async void OnGenreTapped(object sender, TappedEventArgs e)
    {
        GenreCollection genreCollection = e.Parameter as GenreCollection;

        var navigationParameter = new Dictionary<string, object>
        {
            {"GenreName", genreCollection.GenreName}
        };

        await Shell.Current.GoToAsync($"GenrePage", navigationParameter);
    }
}

    