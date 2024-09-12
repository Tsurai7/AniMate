using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;
public class NamesDto
{
    [JsonPropertyName("ru")]
    public string Ru { get; init; }
    [JsonPropertyName("en")]
    public string En { get; init; }
}

