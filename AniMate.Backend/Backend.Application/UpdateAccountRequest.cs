using System.ComponentModel.DataAnnotations;

namespace Backend.Application;

public class UpdateAccountRequest
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    [Url]
    [MaxLength(200)]
    public string ImageUrl { get; set; }
}