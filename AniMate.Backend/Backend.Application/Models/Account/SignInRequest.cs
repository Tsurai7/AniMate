using System.ComponentModel.DataAnnotations;
using Backend.Domain.Models;
using MediatR;

namespace Backend.Application.Models.Account;

public class SignInRequest  : IRequest<AuthToken>
{
    public required string Email { get; init; }  = null!;
    public required string Password { get; init; }  = null!;
}