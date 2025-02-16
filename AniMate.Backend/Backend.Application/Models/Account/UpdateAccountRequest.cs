namespace Backend.Application.Models.Account;

public class UpdateAccountRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}