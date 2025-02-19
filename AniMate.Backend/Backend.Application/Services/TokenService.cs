using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.Services;

public class TokenService
{
    public static AuthToken GenerateToken(string email)
    {
        var claims = new List<Claim> {new(ClaimTypes.Name, email) };

        var expiresIn = DateTime.UtcNow.AddHours(48);

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: expiresIn,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new AuthToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = expiresIn
        };
    }
}