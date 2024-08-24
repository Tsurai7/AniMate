using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime; 
public record EpisodeDto
{
    [JsonProperty("episode")]
    public string Ordinal { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("hls")]
    public HlsDto HlsUrls { get; set; }
}

