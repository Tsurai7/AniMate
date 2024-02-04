using AniMate_backend.Interfaces;
using AniMate_backend.Models;
using Microsoft.AspNetCore.Mvc;


namespace AniMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IAnilibriaService _anilibriaService;
        public AnimeController(IAnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;
        }

        [HttpGet("GetTitle")]
        public async Task<ActionResult<Title>> GetTitle(string titleName)
        {
            var title = await _anilibriaService.GetTitleByCode(titleName);

            return Ok(title);
        }

        [HttpGet("GetTitle")]
        public async Task<ActionResult<List<string>>> GetGenres()
        {
            var genres = await _anilibriaService.GetGenres();

            return Ok(genres);
        }








        //[HttpGet("GetEpisode")]
        //public async Task<ActionResult<EpisodeDto>> GetEpisode(string titleName, int episodeOrdinal)
        //{
        //    using HttpResponseMessage response = await
        //        _httpClient.GetAsync($"{BaseQueryAddress}title?code={titleName}&filter=player.list[{episodeOrdinal}]");

        //    string jsonString = await response.Content.ReadAsStringAsync();

        //    JObject jsonInfo = JObject.Parse(jsonString);

        //    JToken? episodeObject = jsonInfo["player"]["list"][episodeOrdinal];

        //    EpisodeDto episode = new()
        //    {
        //        Ordinal = (int)episodeObject["episode"],
        //        Uuid = (string)episodeObject["uuid"],
        //        Fhd = (string)episodeObject["hls"]["fhd"],
        //        Hd = (string)episodeObject["hls"]["hd"],
        //        Sd = (string)episodeObject["hls"]["sd"]
        //    };

        //    return Ok(episode);
        //}


        //[HttpGet("Search")]
        //public async Task<ActionResult<List<TitleRequestDto>>> Search(string titleName)
        //{
        //    using HttpResponseMessage response = await
        //        _httpClient.GetAsync($"{BaseQueryAddress}title/search?search={titleName}");

        //    string jsonInfo = await response.Content.ReadAsStringAsync();

        //    List<TitleRequestDto> titles = JsonConvert.DeserializeObject<SearchDto>(jsonInfo).Titles;

        //    return Ok(titles);
        //}       
    }
}
