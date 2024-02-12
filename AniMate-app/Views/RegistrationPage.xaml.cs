namespace AniMate_app.Views;

public partial class RegistrationPage : ContentPage
{
    private AppTheme currentTheme = App.Current.RequestedTheme;
    public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void RegistrationButton_Clicked(object sender, EventArgs e)
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

        loginFrame.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
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

    private async void loginLabelTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private void passwordConfirmEntry_Unfocused(object sender, FocusEventArgs e)
    {
        passwordConfirmFrame.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
        passwordConfirmEntry.BackgroundColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void passwordConfirmEntry_Focused(object sender, FocusEventArgs e)
    {
        passwordConfirmFrame.BackgroundColor = Colors.Grey;
        passwordConfirmEntry.BackgroundColor = Colors.Grey;
    }
}