namespace Backend.Application.DTOs.Requests;

public class SignUpRequest
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Passsword { get; init; }
}