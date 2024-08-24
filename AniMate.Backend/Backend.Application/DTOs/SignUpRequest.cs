using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs;

public record SignUpRequest
(
    [Required]
    string Username,
    [Required]
    string Email,
    [Required]
    string Password
);