using AniMate_app.ViewModels;
using AniMate_app.Models.Auth;

namespace AniMate_app.Views;

public partial class SignInPage : ContentPage
{
    private readonly SignInViewModel _viewModel;
    private AppTheme CurrentTheme => App.Current.RequestedTheme;
    
    public SignInPage(SignInViewModel viewModel)
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = _viewModel = viewModel;
	}

    private async void SignInButton_Clicked(object sender, EventArgs e)
    {
        var signInRequest = new SignInRequest
        {
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text
        };

        try
        {
            var profile = await _viewModel.SignIn(signInRequest, CancellationToken.None);
            var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profile},
            };
            await Shell.Current.GoToAsync($"ProfilePage", navigationParameter);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to sign in: {ex.Message}", "OK");
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

    protected override async void OnAppearing()
    {
        var token = await SecureStorage.GetAsync("AccessToken");

        if (string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync("SignInPage");
        }
        else
        {
            await Shell.Current.GoToAsync("ProfilePage"); 
        }
    }
}