namespace AniMate_app.Views;

public partial class LoginPage : ContentPage
{
    private AppTheme currentTheme = App.Current.RequestedTheme;
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    private void usernameEntry_Focused(object sender, FocusEventArgs e)
    {
        loginFrame.BackgroundColor = Colors.Grey;
        usernameEntry.BackgroundColor = Colors.Grey;
    }

    private void usernameEntry_Unfocused(object sender, FocusEventArgs e)
    {
        
        loginFrame.BackgroundColor =  currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
        usernameEntry.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void passwordEntry_Focused(object sender, FocusEventArgs e)
    {
        passwordFrame.BackgroundColor = Colors.Grey;
        passwordEntry.BackgroundColor = Colors.Grey;
    }

    private void passwordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        passwordFrame.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
        passwordEntry.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;

    }

    private async void registrationLabelTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}