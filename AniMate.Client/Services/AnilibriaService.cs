using System.Diagnostics;
using AniMate_app.DTOs.Anime;
using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService
{
    public class AnilibriaService
    {
        private readonly HttpClient _httpClient;

        private const string _url = "https://api.anilibria.tv/v3/";

        public AnilibriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TitleDto> GetTitleByCode(string code)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title?code={code}");
                
                string jsonInfo = await response.Content.ReadAsStringAsync();

                TitleDto titleDto = JsonConvert.DeserializeObject<TitleDto>(jsonInfo);

                return titleDto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new TitleDto();
            }
        }

        public async Task<List<TitleDto>> GetTitlesByCode(List<string> codes, int skip = 0, int count = 6)
        {
            try
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
                using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title/list?code_list={codeList}");

                string jsonInfo = await response.Content.ReadAsStringAsync();

                titles = JsonConvert.DeserializeObject<List<TitleDto>>(jsonInfo);

                return titles;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<TitleDto>();
            }
        }

        public async Task<List<TitleDto>> GetTitlesByName(string name, int skip = 0, int count = 6)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync(
                    $"""{_url}title/search?search={name}&order_by=in_favorites&sort_direction=1&{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");
                
                string jsonInfo = await response.Content.ReadAsStringAsync();

                List<TitleDto> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

                return titles;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<TitleDto>(); 
            }
        }

        public async Task<List<TitleDto>> GetUpdates(int skip = 0, int count = 6)
        {
            try
            {
                using HttpResponseMessage response =
                    await _httpClient.GetAsync($"""{_url}title/updates?{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");

                if (response.IsSuccessStatusCode)
                {
                    string jsonInfo = await response.Content.ReadAsStringAsync();

                    List<TitleDto> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

                    return titles;
                }
                else
                {
                    throw new Exception("Не прошёл запрос");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<TitleDto>(); 
            }
        }

        public async Task<List<string>> GetAllGenres()
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}genres");

                string jsonInfo = await response.Content.ReadAsStringAsync();

                List<string> genres = JsonConvert.DeserializeObject<List<string>>(jsonInfo);

                return genres;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<string>(); 
            }
        }

        public async Task<List<TitleDto>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync(
                    $"""{_url}title/search?genres={genre}&order_by=in_favorites&sort_direction=1{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");

                string jsonInfo = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<TitleDto>(); 
            }
        }
    }
}