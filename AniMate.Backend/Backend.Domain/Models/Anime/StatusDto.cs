using System.Text.Json.Serialization;

namespace Backend.Domain.Models.Anime; 

public class StatusDto
{
    [JsonPropertyName("string")] 
    public string? Status { get; init; }
    [JsonPropertyName("code")] 
    public int Code { get; init; }
}
