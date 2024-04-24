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
        
        _viewModel.GetDataFromApi("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdEB0ZXN0LmNvbSIsImV4cCI6MTcxMzk1Mzk3MCwiaXNzIjoiTXlBdXRoU2VydmVyIiwiYXVkIjoiTXlBdXRoQ2xpZW50In0.4PbXvn1URBymmHmnLZgpg3flmY3rUg4EIu1gqCbv_RY");
        
        
	}
}