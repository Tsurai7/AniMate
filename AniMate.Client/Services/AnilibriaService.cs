using System.Text.Json;
using AniMate_app.DTOs.Anime;

namespace AniMate_app.Services
{
    public class AnilibriaService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        
        private const string _url = "https://api.anilibria.tv/v3/";

        public AnilibriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TitleDto> GetTitleByCode(string code)
        {
            var response = await _httpClient.GetAsync($"{_url}title?code={code}");
            
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

            string codeList = string.Join(",", codesToProcess);
            using HttpResponseMessage response = await _httpClient.GetAsync(
                $"{_url}title/list?code_list={codeList}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TitleDto>>(jsonInfo, SerializerOptions);
        }

        public async Task<List<TitleDto>> GetTitlesByName(string name, int skip = 0, int count = 6)
        {
            var response = await _httpClient.GetAsync(
                $"""{_url}title/search?search={name}&order_by=in_favorites&sort_direction=1&{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");
            
            var jsonInfo = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
        }

        public async Task<List<TitleDto>> GetUpdates(int skip = 0, int count = 6)
        {
            var response = await _httpClient.GetAsync(
                    $"""{_url}title/updates?{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");
            
            var jsonInfo = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
        }

        public async Task<List<string>> GetAllGenres()
        {
            var response = await _httpClient.GetAsync($"{_url}genres");

            var jsonInfo = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<string>>(jsonInfo);
        }

        public async Task<List<TitleDto>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
        {
            var response = await _httpClient.GetAsync(
                $"""{_url}title/search?genres={genre}&order_by=in_favorites&sort_direction=1{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");

            var jsonInfo = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TitlesInfo>(jsonInfo, SerializerOptions).Titles;
        }
    }
}