using AniMate_app.Clients;
using AniMate_app.Interfaces;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class SignUpPage : ContentPage
{
    private readonly SignUpViewModel _viewModel;
    
    private AppTheme currentTheme = App.Current.RequestedTheme;
    
    public SignUpPage(SignUpViewModel viewModel)
	{
		InitializeComponent();
        
        BindingContext = _viewModel = viewModel;
        
        AppShell.SetNavBarIsVisible(this, false);
    }

    private void UsernameEntry_Focused(object sender, FocusEventArgs e)
    {
        UsernameFrame.BorderColor = Colors.Blue;
    }

    private void UsernameEntry_Unfocused(object sender, FocusEventArgs e)
    {
        UsernameFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }
    
    private void EmailEntry_Focused(object sender, FocusEventArgs e)
    {
        EmailFrame.BorderColor = Colors.Blue;
    }

    private void EmailEntry_Unfocused(object sender, FocusEventArgs e)
    {
        EmailFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = Colors.Blue;
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PasswordFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; 
    }

    private async void SignInLabel_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void PasswordConfirmEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PasswordConfirmFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void PasswordConfirmEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordConfirmFrame.BorderColor = Colors.Blue;
    }
    
    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        if (PasswordEntry.Text != PasswordConfirmEntry.Text)
        {
            await DisplayAlert("Error", "Wrong passwords", "OK");
            return;
        }
        
        var response = await _viewModel._authClient.
            SignUp(EmailEntry.Text, UsernameEntry.Text, PasswordEntry.Text);

        if (response.Token != null)
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
            await DisplayAlert("Error", "Unable to sign up", "OK");
        }
    }
}