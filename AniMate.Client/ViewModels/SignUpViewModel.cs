using AniMate_app.Clients;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

public class SignUpViewModel : ObservableObject
{
    public readonly AuthClient _authClient;
    public readonly AccountClient _accountClient;

    public SignUpViewModel(
        AuthClient authClient,
        AccountClient accountClient)
    {
        _authClient = authClient;
        _accountClient = accountClient;
    }
}

