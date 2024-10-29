namespace Backend.API.Controllers.Models.Account;

public class SignUpRequest
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}