using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class EpisodeDto
{
    [JsonPropertyName("episode")]
    public int Ordinal { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("hls")]
    public HlsDto HlsUrls { get; init; }
}

