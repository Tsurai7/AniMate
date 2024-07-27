using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record SignUpRequest
(
    [Required]
    string Username,
    [Required]
    string Email,
    [Required]
    string Password
);