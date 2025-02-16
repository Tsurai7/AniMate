using AniMate_app.ViewModels;
using System.Text.Json;
using AniMate_app.Clients;
using AniMate_app.DTOs.Account;
using AniMate_app.Interfaces;

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
        var email = EmailEntry.Text;
        
        var password = PasswordEntry.Text;

        if (email == "test" && password == "test")
        {
            var profileDto = new ProfileDto("Nikita Desuyo", 
                "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.instagram.com%2Fmeguminfushiguro%2F&psig=AOvVaw3lG69Vz1JTkbsA8WZK9ZIz&ust=1726985201087000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCMC-srWv04gDFQAAAAAdAAAAABAE",
                "nikita@gmail.com", ["Naruto", "Jujutsu Kaisen"], ["Jujutsu Kaisen"]);
            
            var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profileDto},
            };
            
            await Shell.Current.GoToAsync($"ProfilePage", navigationParameter);
            return;
        }

        var response = await _viewModel._authClient.SignIn(email, password);

        if (response.Token is not null)
        {
            var userProfile = await _viewModel._accountClient.GetProfileInfo(response.Token);
            
            Preferences.Default.Set("AccessToken", response.Token);
            
            await Navigation.PushAsync(
                new ProfilePage(new ProfileViewModel(
                    DependencyService.Get<AccountClient>(), 
                    DependencyService.Get<IAnimeClient>(),
                    userProfile)));
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