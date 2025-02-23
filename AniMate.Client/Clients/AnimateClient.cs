using System.Text.Json;
using AniMate_app.DTOs.Anime;

namespace AniMate_app.Clients;

public class AnimateClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    private readonly IHttpClientFactory _httpClientFactory;

    public AnimateClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<List<TitleDto>> GetUpdates(int skip = 0, int count = 6)
    {
        var response = await _httpClientFactory.CreateClient(nameof(AnimateClient)).GetAsync(
            $"title/updates?{(skip > 0 ? $"&skip={skip}" : "")}&limit={skip + count}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        var titles = JsonSerializer.Deserialize<List<TitleDto>>(jsonInfo, SerializerOptions);
        
        return titles;
    }
}