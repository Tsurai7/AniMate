namespace AniMate_app.Models.Auth;

public class SignUpRequest
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}