using AniMate_app.Services.AccountService;
using AniMate_app.Services.AccountService.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(ProfileInfo), "Profile")]
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly AccountService _accountService;

        [ObservableProperty]
        private ProfileDto _profileInfo;

        public ProfileViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }
    }
}