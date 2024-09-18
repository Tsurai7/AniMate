using System.ComponentModel.DataAnnotations;

namespace AccountService.Application.DTOs;

public record SignUpRequest
(
    [Required]
    string Username,
    [Required]
    string Email,
    [Required]
    string Password
);