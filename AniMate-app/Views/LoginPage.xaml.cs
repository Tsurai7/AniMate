namespace AniMate_app.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        // Ваш код для аутентификации пользователя
        // Проверка логина и пароля и т.д.
        // Если вход успешный, выполните нужные действия, например, переход на следующую страницу
        await Navigation.PushAsync(new ProfilePage()); // Переход на основную страницу приложения
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        // Ваш код для обработки нажатия на кнопку регистрации
        // Например, переход на страницу регистрации
        await Navigation.PushAsync(new RegistrationPage());
    }
}