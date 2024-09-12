using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime; 
public class EpisodeDto
{
    [JsonProperty("episode")]
    public string Ordinal { get; init; }

    [JsonProperty("name")]
    public string? Name { get; init; }

    [JsonProperty("hls")]
    public HlsDto HlsUrls { get; init; }
}

