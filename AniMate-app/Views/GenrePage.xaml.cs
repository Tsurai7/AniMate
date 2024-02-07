using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    public readonly GenreViewModel viewModel;

    public GenrePage(GenreViewModel genreViewModel)
    {
        InitializeComponent();

        BindingContext = viewModel = genreViewModel;
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            Title selectedTitle = collectionView.SelectedItem as Title;

            collectionView.SelectedItem = null;

            await Navigation.PushAsync(new PlayerPage(selectedTitle));
        }
    }
}