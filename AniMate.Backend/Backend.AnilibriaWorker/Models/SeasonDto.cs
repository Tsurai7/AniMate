using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class SeasonDto
{
    [JsonPropertyName("string")] 
    public string? Season { get; init; }
    
    public int Code { get; init; }
    
    public int Year { get; init; }
}

