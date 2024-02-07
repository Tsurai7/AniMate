using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;
using System.Collections.ObjectModel;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    public readonly GenreViewModel viewModel;

    public GenrePage(string genre, ObservableCollection<Title> titles, AnilibriaService anilibriaService )
    {
        InitializeComponent();

        BindingContext = viewModel = new GenreViewModel(genre, titles, anilibriaService);
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
}