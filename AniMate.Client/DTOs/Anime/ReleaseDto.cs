using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public record ReleaseDto
{
    [JsonPropertyName("id")]
    string? Id { get; init; }
    [JsonPropertyName("code")]
    string? Code { get; init; }
    [JsonPropertyName("ordinal")]
    string? Ordinal { get; init; }
    [JsonPropertyName("names")]
    public NamesDto? Names { get; init; }
}