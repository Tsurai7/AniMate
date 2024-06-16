using AniMate_app.Services.AccountService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class SignUpViewModel : ObservableObject
    {
        public readonly AccountService _accountService;

        public SignUpViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }
    }
}
