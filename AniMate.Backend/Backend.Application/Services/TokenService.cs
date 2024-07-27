using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    
    private TimeSpan ExpiryDuration = new TimeSpan(1, 30, 0);

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string BuildToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        
        var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}