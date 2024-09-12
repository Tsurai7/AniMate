using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class SeasonDto
{
    [JsonPropertyName("string")] 
    public string Season { get; init; }
    
    public string? Code { get; init; }
    
    public string? Year { get; init; }
}