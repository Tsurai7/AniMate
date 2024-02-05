using AniMate_app.Interfaces;
using AniMate_app.Services.AnilibriaService.Models;
using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService
{
    public class AnilibriaService
    {
        private static readonly HttpClient _httpClient = new();

        private const string _url = "https://api.anilibria.tv/v3/";


        public static async Task<Title> GetTitleByCode(string code)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title?code={code}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            Title title = JsonConvert.DeserializeObject<Title>(jsonInfo);

            return title;
        }


        public static async Task<List<Title>> GetAllTitlesByName(string name)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title/search?search={name}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles; ;

            return titles;
        }


        public static async Task<List<string>> GetAllGenres()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}genres");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<string> genres = JsonConvert.DeserializeObject<List<string>>(jsonInfo);

            return genres;
        }


        public static async Task<List<Title>> GetAllTitlesByGenre(string genre, int skip = 0, int count = 1)
        {
            using HttpResponseMessage response =
                await _httpClient.GetAsync($"""{_url}title/search?genres={genre}{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

            return titles;
        }
    }
}
