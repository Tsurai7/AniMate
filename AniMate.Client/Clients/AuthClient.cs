using System.Text;
using System.Text.Json;
using AniMate_app.Models.Auth;
using SignInRequest = AniMate_app.Models.Auth.SignInRequest;
using SignUpRequest = AniMate_app.Models.Auth.SignUpRequest;

namespace AniMate_app.Clients;

public class AuthClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    private readonly IHttpClientFactory _httpClientFactory;
    
    public AuthClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<AuthResponse> SignIn(SignInRequest request, CancellationToken cancellationToken)
    {
        var jsonContent = JsonSerializer.Serialize(request);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/sign-in", requestContent);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }
    
    public async Task<AuthResponse> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        var jsonContent = JsonSerializer.Serialize(request, SerializerOptions);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/sign-up", requestContent);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }
    
    public async Task<AuthResponse> SignOut()
    {
        throw new NotImplementedException();
    }
}