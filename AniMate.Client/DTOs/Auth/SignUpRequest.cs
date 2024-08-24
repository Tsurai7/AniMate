using System.ComponentModel.DataAnnotations;

namespace AniMate_app.DTOs.Auth;

public record SignUpRequest
(
    [Required]
    string Username,
    [Required]
    string Email,
    [Required]
    string Password
);