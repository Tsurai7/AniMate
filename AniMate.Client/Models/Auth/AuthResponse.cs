namespace AniMate_app.Models.Auth;

public class AuthResponse
{
    public string? Token {get; init;}
    public DateTime Expiration {get; init;}
}