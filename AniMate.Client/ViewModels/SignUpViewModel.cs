using System.Security.Authentication;
using AniMate_app.Clients;
using AniMate_app.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using SignUpRequest = AniMate_app.Models.Auth.SignUpRequest;

namespace AniMate_app.ViewModels;

public class SignUpViewModel : ObservableObject
{
    private readonly AuthClient _authClient;
    private readonly AccountClient _accountClient;

    public SignUpViewModel(
        AuthClient authClient,
        AccountClient accountClient)
    {
        _authClient = authClient;
        _accountClient = accountClient;
    }
    
    public async Task<Profile> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        var authResponse = await _authClient.SignUp(request, cancellationToken);

        if (authResponse.Token != null)
        {
            return await _accountClient.GetProfileInfo(authResponse.Token, cancellationToken);
        }

        throw new AuthenticationException();
    }
}