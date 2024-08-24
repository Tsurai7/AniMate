using AniMate_app.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class SignInViewModel : ObservableObject
    {
        public readonly AccountService _accountService;

        public SignInViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }
    }
}
