namespace Backend.Domain.Models;

public class AuthToken
{
    public string Token { get; init; }
    public DateTime Expiration { get; init; }
}