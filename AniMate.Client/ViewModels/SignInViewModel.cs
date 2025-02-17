using System.Security.Authentication;
using AniMate_app.Clients;
using AniMate_app.Models;
using AniMate_app.Models.Auth;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

public class SignInViewModel : ObservableObject
{
    private readonly AccountClient _accountClient;
    private readonly AuthClient _authClient;

    public SignInViewModel(
        AuthClient authClient,
        AccountClient accountClient)
    {
        _authClient = authClient;
        _accountClient = accountClient;
    }

    public async Task<Profile> SignIn(SignInRequest request, CancellationToken cancellationToken)
    {
        var authResponse = await _authClient.SignIn(request, cancellationToken);

        if (authResponse.Token != null)
        {
            return await _accountClient.GetProfileInfo(authResponse.Token, cancellationToken);
        }

        throw new AuthenticationException();
    }
}