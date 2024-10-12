using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Auth;
using AniMate_app.Interfaces;

namespace AniMate_app.Clients;

public class AccountClient : IAccountClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AccountClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ProfileDto> GetProfileInfo(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"account/profile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).SendAsync(request);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        var profileDto = JsonSerializer.Deserialize<ProfileDto>(jsonInfo, SerializerOptions);
        
        return profileDto;
    }

    public async Task<bool> AddTitleToLiked(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"addTitleToLiked");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveTitleFromLiked(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"removeTitleFromLiked");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).SendAsync(request);
        
        return response.IsSuccessStatusCode;
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

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/signIn", requestContent);
        
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }

    public async Task<bool> AddTitleToHistory(string token, string titleCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"addTitleToHistory");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        request.Content = requestContent;
        
        requestContent.Headers.Add("titleCode", titleCode);

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).SendAsync(request);
        
        return response.IsSuccessStatusCode;
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
        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).PostAsync($"auth/signUp", requestContent);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<AuthResponse>(jsonInfo, SerializerOptions);
    }
}
