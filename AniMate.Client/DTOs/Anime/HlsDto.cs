using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class HlsDto
{
    [JsonPropertyName("fhd")]
    public string Fhd { get; init; }

    [JsonPropertyName("hd")]
    public string Hd { get; init; }

    [JsonPropertyName("sd")]
    public string Sd { get; init; }
}