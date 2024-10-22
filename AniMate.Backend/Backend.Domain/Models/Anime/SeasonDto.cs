using System.Text.Json.Serialization;

namespace Backend.Domain.Models.Anime; 

public class SeasonDto
{
    [JsonPropertyName("string")] 
    public string? Season { get; init; }
    
    public int Code { get; init; }
    
    public int Year { get; init; }
}

