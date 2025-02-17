using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

public class SignInViewModel : ObservableObject
{
    public readonly IAccountClient _accountClient;

    public SignInViewModel(IAccountClient accountClient)
    {
        _accountClient = accountClient;
    }
}

