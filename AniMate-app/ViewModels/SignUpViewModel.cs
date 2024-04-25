using AniMate_app.Services.AccountService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class SignUpViewModel : ObservableObject
    {
        public readonly AccountService AccountService;

        public SignUpViewModel(AccountService accountService)
        {
            AccountService = accountService;
        }
    }
}
