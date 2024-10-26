using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.Handlers;

public class SignUpAccountCommand : IRequest<AuthToken>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}

public class SignUpAccountHandler : IRequestHandler<SignUpAccountCommand, AuthToken>
{
    private readonly AccountRepository _accountRepository;
    
    public SignUpAccountHandler(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<AuthToken> Handle(SignUpAccountCommand request, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);

        if (existingAccount != null)
        {
            throw new InvalidOperationException("Account already exists. Try to sign in");
        }

        var newAccount = new Account
        {
            Username = request.Username,
            Email = request.Email,
            ImageUrl = string.Empty,
            Password = request.Password,
            WatchedTitles = new List<string>(),
            LikedTitles = new List<string>()
        };
        
        await _accountRepository.AddAsync(newAccount);
        
        var claims = new List<Claim> {new(ClaimTypes.Name, request.Email) };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new AuthToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = DateTime.UtcNow.AddMinutes(30)
        };
    }
}