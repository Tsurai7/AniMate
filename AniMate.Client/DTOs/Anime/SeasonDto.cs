using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class SeasonDto
{
    [JsonPropertyName("string")] 
    public string? Season { get; init; }
    [JsonPropertyName("code")] 
    public int Code { get; init; }
    [JsonPropertyName("year")]
    public int Year { get; init; }
}

