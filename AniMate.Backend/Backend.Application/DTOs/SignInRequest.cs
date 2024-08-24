using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs;

public record SignInRequest
(
    [Required]
    string Email,
    [Required]
    string Password
);