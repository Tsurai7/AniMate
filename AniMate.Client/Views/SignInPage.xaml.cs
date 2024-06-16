using AniMate_app.ViewModels;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Auth;

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

    private async void SignInButton_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        
        string password = PasswordEntry.Text;

        AuthResponse response = await _viewModel._accountService.SignIn(email, password);

        if (response is not null)
        {
            ProfileDto profileDto = await _viewModel._accountService.GetProfileInfo(response.AccessToken);
            
            var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profileDto},
            };

            Preferences.Default.Set("AccessToken", response.AccessToken);

            string jsonProfile = JsonSerializer.Serialize(profileDto);
            Preferences.Default.Set("Profile", jsonProfile);

            await Shell.Current.GoToAsync($"ProfilePage", navigationParameter);
        }

        else
        {
            await DisplayAlert("Error", "Wrong credentials", "OK");
        }
    }

    private void EmailEntry_Focused(object sender, FocusEventArgs e)
    {
        LoginFrame.BorderColor = Colors.Blue;
    }

    private void EmailEntry_Unfocused(object sender, FocusEventArgs e)
    {
        LoginFrame.BorderColor =  CurrentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = Colors.Blue;
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = CurrentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private async void SignUpLabel_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"SignUpPage");
        //NavigationPage.SetHasNavigationBar(new RegistrationPage(), true);
        //await Navigation.PushModalAsync(new RegistrationPage());
    }
}