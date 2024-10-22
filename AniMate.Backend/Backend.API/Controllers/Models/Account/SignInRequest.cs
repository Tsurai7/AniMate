using Backend.Domain.Models;
using MediatR;

namespace Backend.API.Controllers.Models.Account;

public class SignInRequest : IRequest<AuthToken> 
{
    public string Email { get; init; }
    public string Password { get; init; }
}