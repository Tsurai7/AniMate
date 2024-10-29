namespace Backend.API.Controllers.Models.Account;

public class SignInRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}