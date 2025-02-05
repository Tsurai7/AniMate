using System.ComponentModel.DataAnnotations;
using Backend.Domain.Models;
using MediatR;

namespace Backend.Application.Models.Account;

public class SignInRequest  : IRequest<AuthToken>
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; init; }  = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string Password { get; init; }  = null!;
}