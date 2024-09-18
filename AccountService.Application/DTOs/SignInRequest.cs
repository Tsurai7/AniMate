using System.ComponentModel.DataAnnotations;

namespace AccountService.Application.DTOs;

public record SignInRequest
(
    [Required]
    string Email,
    [Required]
    string Password
);