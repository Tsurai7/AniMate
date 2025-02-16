using AniMate_app.Clients;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

public class SignInViewModel : ObservableObject
{
    public readonly AuthClient _authClient;
    public readonly AccountClient _accountClient;

    public SignInViewModel(
        AuthClient authClient,
        AccountClient accountClient)
    {
        _authClient = authClient;
        _accountClient = accountClient;
    }
}

