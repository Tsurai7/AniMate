using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application;

public static class AuthOptions
{
    public const string Issuer = "MyAuthServer"; 
    public const string Audience = "MyAuthClient";
    private const string Key = "mysupersecret_secretsecretsecretkey!123";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new (Encoding.UTF8.GetBytes(Key));
}