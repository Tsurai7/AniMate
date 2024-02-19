namespace AniMate_app.Views;

public partial class RegistrationPage : ContentPage
{
    private AppTheme currentTheme = App.Current.RequestedTheme;
    public RegistrationPage()
	{
		InitializeComponent();
        AppShell.SetNavBarIsVisible(this, false);
    }

    private async void RegistrationButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"profilepage");
    }

    private void usernameEntry_Focused(object sender, FocusEventArgs e)
    {
        loginFrame.BorderColor = Colors.Blue;
    }

    private void usernameEntry_Unfocused(object sender, FocusEventArgs e)
    {

        loginFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;

    }

    private void passwordEntry_Focused(object sender, FocusEventArgs e)
    {
        passwordFrame.BorderColor = Colors.Blue;
    }

    private void passwordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        passwordFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; 
    }

    private async void loginLabelTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void passwordConfirmEntry_Unfocused(object sender, FocusEventArgs e)
    {
        passwordConfirmFrame.BorderColor = currentTheme == AppTheme.Dark ? Colors.Black : Colors.White; ;
    }

    private void passwordConfirmEntry_Focused(object sender, FocusEventArgs e)
    {
        passwordConfirmFrame.BorderColor = Colors.Blue;
    }
}