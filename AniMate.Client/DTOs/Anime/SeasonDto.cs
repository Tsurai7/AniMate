using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class SeasonDto
{
    [JsonPropertyName("string")] 
    public string? Season { get; init; }
    
    public int Code { get; init; }
    
    public int Year { get; init; }
}

