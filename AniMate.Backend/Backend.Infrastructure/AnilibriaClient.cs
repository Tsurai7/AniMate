using System.Text.Json;
using Backend.Domain.Models.Anime;
using Microsoft.Extensions.Logging;

namespace Backend.Infrastructure;

public class AnilibriaClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AnilibriaClient> _logger;
    
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AnilibriaClient(IHttpClientFactory httpClientFactory, ILogger<AnilibriaClient> logger)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(AnilibriaClient));
        _logger = logger;
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
            return [];
        }
    }

    public async Task<List<TitleDto>> GetUpdates(int since, int page, CancellationToken cancellationToken = default)
    {
        const int limit = 20;
        try
        {
            var url = $"title/updates?since={since}&page={page}&limit={limit}";
            var response = await _httpClient.GetAsync(url, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Request failed with status code {StatusCode}: {ReasonPhrase}. URL: {Url}",
                    response.StatusCode, response.ReasonPhrase, url);

                return new List<TitleDto>(); 
            }

            var jsonInfo = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions)?.Titles ?? new List<TitleDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while requesting for updates to Anilibria");
            return new List<TitleDto>();
        }
    }
}