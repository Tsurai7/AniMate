using AniMate_app.Services.AuthService;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(_token), "Token")]
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        
        private string _username;
        private string _token;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;
        }
        
        public string Token
        {
            set
            {
                _token = value;
                GetDataFromApi(_token);
            }
        }

        public async void GetDataFromApi(string token)
        {
            Preferences.Default.Set("AccessToken", token);
            string newUsername = await _authService.GetStringFromApi(token);
            Username = newUsername; // Обновляем значение Username
        }
    }
}