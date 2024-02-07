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

    private async void OnEntryChanged(object sender, EventArgs e)
    {
        _searchText = entry.Text;

        await viewModel.FindTitles(_searchText);
    }

    private void ClearTextEntry(object sender, EventArgs e)
    {
        entry.Text = string.Empty;

        viewModel.ClearSearchData();
    }
}