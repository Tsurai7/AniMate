using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models.Account;

public class UpdateAccountRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [Url] 
    [MaxLength(200)] 
    public string ImageUrl { get; set; } = null!;
}