using System.ComponentModel.DataAnnotations;

namespace AniMate_app.DTOs.Auth;

public record SignInRequest
(
    [Required]
    string Email,
    [Required]
    string Password
);