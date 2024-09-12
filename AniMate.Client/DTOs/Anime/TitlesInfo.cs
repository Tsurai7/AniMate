using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class TitlesInfo
{
    [JsonPropertyName("list")] 
    public List<TitleDto> Titles { get; init; }
}
