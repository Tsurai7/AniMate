using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class StatusDto
{
    [JsonPropertyName("string")] 
    public string? Status { get; init; }
    [JsonPropertyName("code")] 
    public int Code { get; init; }
}
