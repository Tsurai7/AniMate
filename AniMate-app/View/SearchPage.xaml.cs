namespace AniMate_app.View;

using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModel;
using System.Threading.Tasks;

public partial class SearchPage : ContentPage
{
    private readonly SearchViewModel viewModel;

    public SearchPage(SearchViewModel searchViewModel)
	{
        InitializeComponent();

        BindingContext = viewModel = searchViewModel;
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        viewModel.FindTitles(entry.Text);
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            Title selectedTitle = collectionView.SelectedItem as Title;

            collectionView.SelectedItem = null;

            Navigation.PushAsync(new PlayerPage(selectedTitle));
        }
    }
}