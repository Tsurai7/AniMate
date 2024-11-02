using System.ComponentModel.DataAnnotations;

namespace Backend.API.Controllers.Models.Account;

public class SignUpRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; init; } = null!;
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; init; }  = null!;
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; init; }  = null!;
}