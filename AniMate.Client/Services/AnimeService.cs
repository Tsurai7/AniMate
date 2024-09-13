using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AniMate_app.DTOs.Anime;

namespace AniMate_app.Services;

public class AnimeService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AnimeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TitleDto> GetTitleByCode(string code)
    {
        var response = await _httpClient.GetAsync($"title?code={code}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        var titleDto = JsonSerializer.Deserialize<TitleDto>(jsonInfo, SerializerOptions);

        return titleDto;
    }

    public async Task<List<TitleDto>> GetTitlesByCode(List<string> codes, int skip = 0, int count = 6)
    {
        if (codes == null || !codes.Any())
        {
            return new List<TitleDto>();
        }

        List<TitleDto> titles = new();
        var codesToProcess = codes.Skip(skip).Take(count).Where(code => code != null).ToList();

        if (!codesToProcess.Any())
        {
            return titles;
        }

        var codeList = string.Join(",", codesToProcess);
        var response = await _httpClient.GetAsync($"title/list?code_list={codeList}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<TitleDto>>(jsonInfo, SerializerOptions);
    }

    public async Task<List<TitleDto>> GetTitlesByName(string name, int skip = 0, int count = 6)
    {
        var response = await _httpClient.GetAsync(
            $"title/search?search={name}&order_by=in_favorites&sort_direction=1&{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
    }

    public async Task<List<TitleDto>> GetUpdates(int skip = 0, int count = 6)
    {
        var response = await _httpClient.GetAsync(
            $"title/updates?{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
    }

    public async Task<List<string>> GetAllGenres()
    {
        var response = await _httpClient.GetAsync($"genres");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<string>>(jsonInfo);
    }

    public async Task<List<TitleDto>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
    {
        var response = await _httpClient.GetAsync(
            $"title/search?genres={genre}&order_by=in_favorites&sort_direction=1{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}");

        var jsonInfo = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
    }
}