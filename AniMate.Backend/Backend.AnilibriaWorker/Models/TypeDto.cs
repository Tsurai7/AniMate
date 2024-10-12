using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class TypeDto
{
    [JsonPropertyName("full_string")] 
    public string? FullInfo { get; init; }
    [JsonPropertyName("string")] 
    public string? Type { get; init; }
    [JsonPropertyName("episodes")] 
    public int? Series { get; init; } 
}
