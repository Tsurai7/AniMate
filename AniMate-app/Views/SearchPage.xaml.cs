using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class SearchPage : ContentPage
{
    private readonly SearchViewModel viewModel;

    private string _searchText = string.Empty; 

    public SearchPage(SearchViewModel searchViewModel)
	{
        InitializeComponent();

        BindingContext = viewModel = searchViewModel;
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        string currentText = entry.Text;

        if(currentText != _searchText)
        {
            viewModel.Titles.Clear();

            viewModel.FindTitles(entry.Text);

            entry.Unfocus();
        }
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

    private void OnEntryChanged(object sender, TextChangedEventArgs e)
    {
        _searchText = entry.Text;

        viewModel.Titles.Clear();

        viewModel.FindTitles(entry.Text);

        entry.Unfocus();   
    }
}