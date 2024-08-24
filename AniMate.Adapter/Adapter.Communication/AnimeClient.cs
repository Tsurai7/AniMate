using System.Text.Json;
using Adapter.Domain.Interfaces;
using Adapter.Domain.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Adapter.Communication;

public class AnimeClient : IAnimeClient
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

    public Task<GetTitleResponse> GetTitleByCode(string code)
    {
        throw new NotImplementedException();
    }

    public Task<GetTitlesResponse> GetTitlesByGenre(string genre)
    {
        throw new NotImplementedException();
    }

    public Task<GetTitlesUpdatesResponse> GetUpdatedTitles()
    {
        throw new NotImplementedException();
    }

    public Task<SearchTitlesResponse> SearchTitlesByName(string name)
    {
        throw new NotImplementedException();
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