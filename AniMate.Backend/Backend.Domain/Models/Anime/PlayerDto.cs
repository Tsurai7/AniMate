using System.Text.Json.Serialization;

namespace Backend.Domain.Models.Anime; 
public class PlayerDto
{
    public string? AlternativePlayer { get; init; }
    public string Host { get; init; }
    [JsonPropertyName("list")]
    public Dictionary<string, EpisodeDto> Episodes { get; init; }
}
