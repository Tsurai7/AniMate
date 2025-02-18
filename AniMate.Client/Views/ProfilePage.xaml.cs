using AniMate_app.DTOs.Anime;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _viewModel;

	public ProfilePage(ProfileViewModel profileViewModel)
	{
        Shell.SetNavBarIsVisible(this, false);
        InitializeComponent();
        BindingContext = _viewModel = profileViewModel;
    }
    

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (collectionView.SelectedItem != null)
        {
            var selectedTitleDto = collectionView.SelectedItem as TitleDto;

            collectionView.SelectedItem = null;

            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collectionView.SelectedItem}
            };

            await Shell.Current.GoToAsync($"TitlePage", navigationParameter);
        }
    }
    
    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage());
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}