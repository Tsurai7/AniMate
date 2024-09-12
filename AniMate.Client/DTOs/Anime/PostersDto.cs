using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class PostersDto
{
    [JsonPropertyName("small")]
    public PosterSize Small { get; init; }
    [JsonPropertyName("medium")]
    public PosterSize Medium { get; init; }
    [JsonPropertyName("original")]
    public PosterSize Original { get; init; }
}
