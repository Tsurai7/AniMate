using AniMate_backend.Models;
using Newtonsoft.Json;

namespace AniMate_backend.Dtos
{
    public record PlayerDto
    {
        [JsonProperty("alternative_player")]
        public string AlternativePlayer { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("list")]
        public Dictionary<string, Episode>? Episodes { get; set; }
    }
}
