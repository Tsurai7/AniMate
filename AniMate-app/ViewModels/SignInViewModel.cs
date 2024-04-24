using AniMate_app.Services.AccountService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class SignInViewModel : ObservableObject
    {
        public readonly AccountService AccountService;

        public SignInViewModel(AccountService accountService)
        {
            AccountService = accountService;
        }
    }
}
