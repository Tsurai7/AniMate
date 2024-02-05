using AniMate_backend.Interfaces;
using AniMate_backend.Models;
using Newtonsoft.Json;


namespace AniMate_backend.Services
{
    public class AnilibriaService : IAnilibriaService
    {
        private readonly HttpClient _httpClient;

        private const string _url = "https://api.anilibria.tv/v3/";

        public AnilibriaService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<Title> GetTitleByCode(string code)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title?code={code}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            Title title = JsonConvert.DeserializeObject<Title>(jsonInfo);

            return title;
        }


        public async Task<List<Title>> GetAllTitlesByName(string name)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title/search?search={name}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles; ;

            return titles;
        }


        public async Task<List<string>> GetAllGenres()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}genres");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<string> genres = JsonConvert.DeserializeObject<List<string>>(jsonInfo);

            return genres;
        }


        public async Task<List<Title>> GetAllTitlesByGenre(string genre)
        {
            using HttpResponseMessage response =
                await _httpClient.GetAsync($"{_url}title/search?genres={genre}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<Title> titles = JsonConvert.DeserializeObject<TitlesInfo>(jsonInfo).Titles;

            return titles;
        }
    }
}
