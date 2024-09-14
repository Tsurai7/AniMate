using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;
public record PlayerDto
{
    public string? AlternativePlayer { get; init; }
    public string Host { get; init; }
    [JsonPropertyName("list")]
    public Dictionary<string, EpisodeDto> Episodes { get; init; }
}
