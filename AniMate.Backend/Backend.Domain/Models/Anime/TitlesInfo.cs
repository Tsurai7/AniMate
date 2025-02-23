using System.Text.Json.Serialization;

namespace Backend.Domain.Models.Anime; 

public class TitlesInfo
{
    [JsonPropertyName("list")]
    public List<TitleDto> Titles { get; init; }
}
