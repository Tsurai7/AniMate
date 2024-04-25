using AniMate_app.Services.AccountService;
using AniMate_app.Services.AccountService.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Token), "Token")]
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly AccountService _accountService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _profileImage;

        [ObservableProperty]
        private string _token;

        public ProfileViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public async void LoadProfileData(string token)
        {
            ProfileDto profileDto = await _accountService.GetProfileInfo(token);
            
            Username = profileDto.Username;

            ProfileImage = profileDto.ProfileImage;
        }
    }
}