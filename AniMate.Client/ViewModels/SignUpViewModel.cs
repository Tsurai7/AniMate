using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

public class SignUpViewModel : ObservableObject
{
    public readonly IAccountClient _accountClient;

    public SignUpViewModel(IAccountClient accountClient)
    {
        _accountClient = accountClient;
    }
}

