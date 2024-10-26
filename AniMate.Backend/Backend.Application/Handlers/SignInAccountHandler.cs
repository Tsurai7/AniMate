using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.Handlers;

public class SignInAccountCommand : IRequest<AuthToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignInAccountHandler : IRequestHandler<SignInAccountCommand, AuthToken>
{
    private readonly AccountRepository _accountRepository;
    
    public SignInAccountHandler(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<AuthToken> Handle(SignInAccountCommand request, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);

        if (existingAccount is null)
        {
            throw new InvalidOperationException("No account found. Try to sign up.");
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.Email)
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new AuthToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = DateTime.UtcNow.AddMinutes(30)
        };
    }
}