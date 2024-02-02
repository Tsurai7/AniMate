using AniMate_app.Dto;
using Newtonsoft.Json.Linq;

namespace AniMate_app.Services
{
    class AnimeService
    {
        private const string BaseQueryAddress =
            "https://api.anilibria.tv/v3/";

        private readonly HttpClient _httpClient = new();

        public async Task<TitleDto> GetTitle(string titleCode)
        {
            using HttpResponseMessage response = await
                _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleCode}");

            string jsonString = await response.Content.ReadAsStringAsync();

            JObject jsonInfo = JObject.Parse(jsonString);

            TitleDto title = new()
            {
                RuName = jsonInfo["names"]["ru"].ToString(),
                Genres = jsonInfo["genres"].ToObject<List<string>>(),
                Player = jsonInfo["player"]["host"].ToString(),
                Episodes = jsonInfo["player"]["episodes"]
                    .ToObject<List<EpisodeDto>>()
            };

            return title;
        }
    }
}
