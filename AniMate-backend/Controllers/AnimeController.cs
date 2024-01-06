using AniMate_backend.Models.GetEpisode;
using AniMate_backend.Models.TitleInfo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AniMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private const string BaseQueryAddress =
            "https://api.anilibria.tv/v3/";

        private readonly HttpClient _httpClient = new();


        [HttpGet("GetTitle")]
        public async Task<ActionResult<TitleRequestDto>> GetTitle(string titleName)
        {
            using HttpResponseMessage response = await
                _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleName}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            TitleRequestDto title = JsonConvert.DeserializeObject<TitleRequestDto>(jsonInfo);

            return Ok(title);
        }

        [HttpGet("GetEpisode")]
        public async Task<ActionResult<EpisodeDto>> GetEpisode(string titleName, int episodeOrdinal)
        {
            using HttpResponseMessage response = await
                _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleName}&filter=player.list[{episodeOrdinal}]");

            string jsonString = await response.Content.ReadAsStringAsync();

            JObject jsonInfo = JObject.Parse(jsonString);

            JToken episodeObject = jsonInfo["player"]["list"][episodeOrdinal];

            EpisodeDto episode = new()
            {
                Ordinal = (int)episodeObject["episode"],
                Uuid = (string)episodeObject["uuid"],
                Fhd = (string)episodeObject["hls"]["fhd"],
                Hd = (string)episodeObject["hls"]["hd"],
                Sd = (string)episodeObject["hls"]["sd"]
            };

            return Ok(episode);
        }


        [HttpGet("Search")]
        public async Task<ActionResult<List<TitleRequestDto>>> Search(string titleName)
        {
            using HttpResponseMessage response = await
                _httpClient.GetAsync($"{BaseQueryAddress}title/search?search={titleName}");

            string jsonInfo = await response.Content.ReadAsStringAsync();

            List<TitleRequestDto> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

            return Ok(titles);
        }       
    }
}
