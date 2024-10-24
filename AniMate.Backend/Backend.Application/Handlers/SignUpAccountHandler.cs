using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Domain.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.Handlers;

public class SignUpAccountCommand : IRequest<AuthToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignUpAccountHandler : IRequestHandler<SignUpAccountCommand, AuthToken>
{
    public Task<AuthToken> Handle(SignUpAccountCommand request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim> {new(ClaimTypes.Name, request.Email) };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Task.FromResult(new AuthToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = DateTime.UtcNow.AddMinutes(30)
        });
    }
}