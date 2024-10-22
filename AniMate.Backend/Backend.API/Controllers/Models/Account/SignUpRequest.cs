using Backend.Domain.Models;
using MediatR;

namespace Backend.API.Controllers.Models.Account;

public class SignUpRequest : IRequest<AuthToken>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}