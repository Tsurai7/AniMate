using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class StatusDto
{
    [JsonPropertyName("string")] 
    public string Status { get; init; }
    
    public string Code { get; init; }
}