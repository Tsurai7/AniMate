using AniMate_app.Services;
using AniMate_app.Services.AuthService.Dtos;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class SignInPage : ContentPage
{
    private readonly SignInViewModel _viewModel;
    private AppTheme CurrentTheme => App.Current.RequestedTheme;
    
    public SignInPage(SignInViewModel viewModel)
	{
		InitializeComponent();
        
        BindingContext = _viewModel = viewModel;
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        SignInResponse response = await _viewModel.AuthService.SignIn(email, password);

        if (response is not null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"Token", response.access_token},
                {"Username", response.email},
            };
            
            Preferences.Default.Set("AccessToken", response.access_token);
        
            await Shell.Current.GoToAsync($"profilepage", navigationParameter);
            
        }
        else 
            await DisplayAlert("Error", "Wrong credentials", "OK");
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