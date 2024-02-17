using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    public readonly GenreViewModel viewModel;

    public GenrePage(string genreName, AnilibriaService anilibriaService )
    {
        InitializeComponent();

        BindingContext = viewModel = new GenreViewModel(genreName, anilibriaService);
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            Title selectedTitle = collectionView.SelectedItem as Title;

            collectionView.SelectedItem = null;

            await Navigation.PushAsync(new TitlePage(selectedTitle));
        }
    }

    private async void OnAppearing(object sender, EventArgs e)
    {
        await viewModel.LoadMoreTitles();
    }
}