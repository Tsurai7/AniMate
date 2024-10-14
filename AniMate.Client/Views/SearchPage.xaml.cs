using AniMate_app.ViewModels;
using AniMate_app.Views.Components;
using CommunityToolkit.Maui.Views;

namespace AniMate_app.Views;

public partial class SearchPage : ContentPage
{
    private readonly SearchViewModel _viewModel;

    private string _searchText = string.Empty; 

    public SearchPage(SearchViewModel searchViewModel)
	{
        InitializeComponent();

        BindingContext = _viewModel = searchViewModel;
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            collectionView.SelectedItem = null;

            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collectionView.SelectedItem}
            };

            await Shell.Current.GoToAsync($"TitlePage", navigationParameter);
        }
    }

    private async void OnSearch(object sender, EventArgs e)
    {
        _searchText = searchBar.Text;

        searchBar.Unfocus();

        await _viewModel.FindTitles(_searchText);
    }

    void OnFiltredButtonClicked(object sender, EventArgs args)
    {
      this.ShowPopup(new FilterPopUp());
    }

    private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        if(searchBar.Text == string.Empty)
        {
            searchBar.Text = string.Empty;

            _viewModel.ClearSearchData();
        }
    }
}