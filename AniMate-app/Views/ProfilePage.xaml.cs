using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _viewModel;
	public ProfilePage(ProfileViewModel profileViewModel)
	{
        AppShell.SetNavBarIsVisible(this, false);
        
        InitializeComponent();
        
        BindingContext = _viewModel = profileViewModel;
        
        _viewModel.GetDataFromApi(Preferences.Default.Get<string>("AccessToken", default));
	}
}