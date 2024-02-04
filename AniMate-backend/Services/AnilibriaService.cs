using AniMate_backend.Interfaces;
using AniMate_backend.Models;
using Newtonsoft.Json;


namespace AniMate_backend.Services
{
    public class AnilibriaService : IAnilibriaService
    {
        private readonly HttpClient _httpClient;

        private readonly string _url = "https://api.anilibria.tv/v3/";

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

        public async Task<List<string>> GetGenres()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}genres");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<string> genres = JsonConvert.DeserializeObject<List<string>>(jsonInfo);

            return genres;
        }

        public Task<List<Title>> GetTitlesByGenre(string genre)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Title>> GetTitlesByGenre(string genre, int skip = 0, int count = 1)
        //{
        //    using HttpResponseMessage response = await _httpClient.GetAsync($"""{_url}title/search?genres={genre}{(skip > 0 ? $"&after={skip}" : "")}&limit={skip + count}""");

        //    string jsonInfo = await response.Content.ReadAsStringAsync();

        //    List<Title> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

        //    return titles;
        //}

        //public async Task<Episode> GetEpisode(string titleName, int episodeOrdinal)
        //{
        //    using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title?code={titleName}&filter=player.list[{episodeOrdinal}]");

        //    string jsonString = await response.Content.ReadAsStringAsync();

        //    JObject jsonInfo = JObject.Parse(jsonString);

        //    JToken episodeObject = jsonInfo["player"]["list"][episodeOrdinal];

        //    Episode episode = new()
        //    {
        //        Ordinal = (int)episodeObject["episode"],
        //        Uuid = (string)episodeObject["uuid"],
        //        Fhd = (string)episodeObject["hls"]["fhd"],
        //        Hd = (string)episodeObject["hls"]["hd"],
        //        Sd = (string)episodeObject["hls"]["sd"]
        //    };

        //    return episode;
        //}

        //public async Task<List<Title>> Search(string titleName)
        //{
        //    using HttpResponseMessage response = await _httpClient.GetAsync($"{_url}title/search?search={titleName}");

        //    string jsonInfo = await response.Content.ReadAsStringAsync();

        //    List<Title> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

        //    return titles;
        //}
    }
}
