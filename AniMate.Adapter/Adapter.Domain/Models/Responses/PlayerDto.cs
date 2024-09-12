using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class PlayerDto
{
    public string AlternativePlayer { get; init; }
        
    public string Host { get; init; }
    
    [JsonPropertyName("list")]
    public Dictionary<string, EpisodeDto> Episodes { get; init; }
}