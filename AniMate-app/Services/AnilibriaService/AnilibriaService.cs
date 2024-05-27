using System.Diagnostics;
using AniMate_app.Services.AnilibriaService.Models;
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

        public async Task<Title> GetTitleByCode(string code)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title?code={code}");
                
                string jsonInfo = await response.Content.ReadAsStringAsync();

                Title title = JsonConvert.DeserializeObject<Title>(jsonInfo);

                return title;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new Title();
            }
        }

        public async Task<List<Title>> GetTitlesByName(string name, int skip = 0, int count = 6)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync(
                    $"""{_url}title/search?search={name}&order_by=in_favorites&sort_direction=1&{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");
                
                string jsonInfo = await response.Content.ReadAsStringAsync();

                List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

                return titles;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<Title>(); 
            }
        }

        public async Task<List<Title>> GetUpdates(int skip = 0, int count = 6)
        {
            try
            {
                using HttpResponseMessage response =
                    await _httpClient.GetAsync($"""{_url}title/updates?{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");
                
                string jsonInfo = await response.Content.ReadAsStringAsync();

                List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

                return titles;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                return new List<Title>(); 
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

        public async Task<List<Title>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
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
                return new List<Title>(); 
            }
        }
    }
}