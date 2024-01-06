using Newtonsoft.Json;

namespace AniMate_backend.Models.TitleInfo
{
    public record EpisodeRequestDto
    {
        [JsonProperty("uuid")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("episode")]
        public int Ordinal { get; set; }

        [JsonProperty("hls")]
        public Hls HlsUrls { get; set; }

    }

    public record Hls
    {
        [JsonProperty("fhd")]
        public string? Fhd { get; set; }

        [JsonProperty("hd")]
        public string? Hd { get; set; }

        [JsonProperty("sd")]
        public string? Sd { get; set; }
    }
}
