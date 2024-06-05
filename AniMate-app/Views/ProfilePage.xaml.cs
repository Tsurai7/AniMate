using AniMate_app.ViewModels;
using AniMate_app.Services.AnilibriaService.Models;
namespace AniMate_app.Views;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _viewModel;

	public ProfilePage(ProfileViewModel profileViewModel)
	{
        AppShell.SetNavBarIsVisible(this, false);
        
        InitializeComponent();
        
        BindingContext = _viewModel = profileViewModel;
	}

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            Title selectedTitle = collectionView.SelectedItem as Title;

            collectionView.SelectedItem = null;

            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collectionView.SelectedItem}
            };

            await Shell.Current.GoToAsync($"TitlePage", navigationParameter);

        }
    }

    private async void OnAppearing(object sender, EventArgs e)
    {
        await _viewModel.LoadMoreContent();
    }
}