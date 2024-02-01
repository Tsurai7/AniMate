using Newtonsoft.Json;

namespace AniMate_app.Anilibria
{
    public record TitleRequestDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("names")]
        public NamesDto Names { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("player")]
        public PlayerDto Player { get; set; }
    }
}
