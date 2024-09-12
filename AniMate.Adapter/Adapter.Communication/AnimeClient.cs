using System.Text.Json;
using Adapter.Domain.Interfaces;
using Adapter.Domain.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Adapter.Communication;

internal class AnimeClient : IAnimeClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AnimeClient> _logger;
    
    public AnimeClient(
        IHttpClientFactory httpClientFactory,
        ILogger<AnimeClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public async Task<Title> GetTitleByCode(string code)
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(AnimeClient));
        
        var response = await httpClient.GetAsync($"title?code={code}");
            
        var responseJson = await response.Content.ReadAsStringAsync();

        var result = TryDeserialize<Title>(responseJson);

        return result;
    }

    public async Task<List<Title>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(AnimeClient));

        var response = await httpClient.GetAsync(
            $"title/search?genres={genre}&order_by=in_favorites&sort_direction=1{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");
        
        var responseJson = await response.Content.ReadAsStringAsync();

        var result = TryDeserialize<List<Title>>(responseJson);

        return result;
    }

    public async Task<List<Title>> GetUpdatedTitles(int skip = 0, int count = 6)
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(AnimeClient));
        
        var response = await httpClient.GetAsync(
            $"title/updates?{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");
        
        var responseJson = await response.Content.ReadAsStringAsync();

        var result = TryDeserialize<List<Title>>(responseJson);

        return result;
    }

    public async Task<List<Title>> SearchTitlesByName(string name, int skip = 0, int count = 6)
    {
        var httpClient = _httpClientFactory.CreateClient(nameof(AnimeClient));
        
        var response = await httpClient.GetAsync(
            $"title/search?search={name}&order_by=in_favorites&sort_direction=1&{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");
            
        var responseJson = await response.Content.ReadAsStringAsync();

        var result = TryDeserialize<List<Title>>(responseJson);

        return result;
    }

    private static T TryDeserialize<T>(string json) where T : new()
    {
        try
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }
        catch (Exception)
        {
            return new T();
        }
    }
}