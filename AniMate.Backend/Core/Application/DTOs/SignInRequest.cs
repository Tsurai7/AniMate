using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record SignInRequest
(
    [Required]
    string Email,
    [Required]
    string Password
);