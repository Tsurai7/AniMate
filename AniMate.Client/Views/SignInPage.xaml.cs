using System;
using System.Collections.Generic;
using AniMate_app.ViewModels;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;

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
        }

        var response = await _viewModel._accountClient.SignIn(email, password);

        if (response is not null )
        {
            var profileDto = await _viewModel._accountClient.GetProfileInfo(response.AccessToken);
            
            var navigationParameter = new Dictionary<string, object>
            {
                {"Profile", profileDto},
            };

            Preferences.Default.Set("AccessToken", response.AccessToken);

            var jsonProfile = JsonSerializer.Serialize(profileDto);
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