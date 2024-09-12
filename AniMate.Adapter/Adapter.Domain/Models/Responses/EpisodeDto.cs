using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class EpisodeDto
{
    [JsonPropertyName("episode")]
    public string Ordinal { get; set; }
    
    public string Name { get; set; }
    
    public HlsDto HlsUrls { get; set; }
}