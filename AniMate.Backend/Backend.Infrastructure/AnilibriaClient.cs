using System.Text.Json;
using Backend.Domain.Models.Anime;

namespace Backend.Infrastructure;

public class AnilibriaClient
{
    private readonly HttpClient _httpClient;
    
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AnilibriaClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(AnilibriaClient));
    }

    public async Task<List<string>> GetAllGenres()
    {
        var response = await _httpClient.GetAsync($"genres");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<string>>(jsonInfo);
    }
    
    public async Task<List<TitleDto>> GetTitlesByGenre(string genre, int page)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"title/search?genres={genre}&page={page}");
    
            var jsonInfo = await response.Content.ReadAsStringAsync();
    
            var result = JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
            return result;
        }
        catch (Exception e)
        {
            return new List<TitleDto>();
        }
    }
}