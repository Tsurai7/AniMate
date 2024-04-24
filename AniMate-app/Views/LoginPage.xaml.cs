using AniMate_app.Services;
using AniMate_app.Services.AuthService.Dtos;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    private AppTheme CurrentTheme => App.Current.RequestedTheme;
    
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        
        BindingContext = _viewModel = viewModel;
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        SignInResponse response = await _viewModel._authService.SignIn(email, password);
        
        var navigationParameter = new Dictionary<string, object>
        {
            {"Token", response.access_token},
            {"Username", response.username},
        };
        
        await Shell.Current.GoToAsync($"profilepage", navigationParameter);
    }

    private void usernameEntry_Focused(object sender, FocusEventArgs e)
    {
        LoginFrame.BorderColor = Colors.Blue;
    }

    private void usernameEntry_Unfocused(object sender, FocusEventArgs e)
    {
        LoginFrame.BorderColor =  CurrentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void passwordEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = Colors.Blue;
    }

    private void passwordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = CurrentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private async void registrationLabelTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"registrationpage");
        //NavigationPage.SetHasNavigationBar(new RegistrationPage(), true);
        //await Navigation.PushModalAsync(new RegistrationPage());
    }
}