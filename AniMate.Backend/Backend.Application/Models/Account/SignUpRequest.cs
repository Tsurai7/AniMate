using Backend.Domain.Models;
using MediatR;

namespace Backend.Application.Models.Account;

public class SignUpRequest : IRequest<AuthToken>
{
    public string Username { get; init; } = null!;
    public required string Email { get; init; }  = null!;
    public string Password { get; init; }  = null!;
}