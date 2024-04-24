using AniMate_app.Services.AccountService;
using AniMate_app.Services.AccountService.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(_token), "Token")]
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly AccountService _accountService;
        
        private string _username;

        [ObservableProperty]
        private string _profileImage;
        
        private string _token;
        
        public string Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ProfileViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public async void GetDataFromApi(string token)
        {
            ProfileDto profileDto = await _accountService.GetProfileInfo(token);
            
            Username = profileDto.Username;

            ProfileImage = profileDto.ProfileImage;
        }
    }
}