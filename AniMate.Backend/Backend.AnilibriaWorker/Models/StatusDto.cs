using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class StatusDto
{
    [JsonPropertyName("string")] 
    public string? Status { get; init; }
    [JsonPropertyName("code")] 
    public int Code { get; init; }
}
