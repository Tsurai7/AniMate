using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class TitlesInfo
{
    [JsonPropertyName("list")] 
    public List<TitleDto> Titles { get; init; }
}
