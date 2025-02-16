using System.Text;
using System.Text.Json;
using AniMate_app.DTOs.Auth;

namespace AniMate_app.Clients;

public class AuthClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<AuthResponse> SignUp(string email, string password, string username)
    {
        var authData = new
        {
            username,
            email,
            password,
        };

        var jsonContent = JsonSerializer.Serialize(authData, SerializerOptions);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/sign-up", requestContent);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }
    
    public async Task<AuthResponse> SignIn(string email, string password)
    {
        var authData = new
        {
            email,
            password
        };

        var jsonContent = JsonSerializer.Serialize(authData);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/sign-in", requestContent);
        
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }
}