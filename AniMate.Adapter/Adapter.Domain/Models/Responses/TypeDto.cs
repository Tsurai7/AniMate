using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class TypeDto
{
    [JsonPropertyName("full_string")]
    public string FullInfo { get; init; }
        
    public string Type { get; init; }
        
    public string Series { get; init; }
        
    public string Length { get; init; }
}