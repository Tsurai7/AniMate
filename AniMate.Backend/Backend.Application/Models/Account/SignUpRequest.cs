using Backend.Domain.Models;
using MediatR;

namespace Backend.Application.Models.Account;

public class SignUpRequest   : IRequest<AuthToken>
{
    public string UserName { get; init; } = null!;
    public string Email { get; init; }  = null!;
    public string Password { get; init; }  = null!;
}