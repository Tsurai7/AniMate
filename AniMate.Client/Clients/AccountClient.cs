using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AniMate_app.Models;

namespace AniMate_app.Clients;

public class AccountClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Profile> GetProfileInfo(string token, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"account/profile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClientFactory.CreateClient(nameof(AccountClient)).SendAsync(request);
        var jsonInfo = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Profile>(jsonInfo, SerializerOptions);
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
}
