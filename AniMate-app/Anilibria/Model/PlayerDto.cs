using Newtonsoft.Json;

namespace AniMate_app.Anilibria
{
    public record PlayerDto
    {
        [JsonProperty("alternative_player")]
        public string AlternativePlayer { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("list")]
        public Dictionary<string, EpisodeRequestDto> Episodes { get; set; }
    }
}
