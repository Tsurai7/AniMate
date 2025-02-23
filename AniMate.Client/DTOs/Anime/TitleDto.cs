using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime; 
public class TitleDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("names")]
    public NamesDto Names { get; set; }

    [JsonPropertyName("franchises")]
    public List<FranchiseDto>? Franchises { get; set; }

    [JsonPropertyName("status")]
    public StatusDto Status { get; set; }

    [JsonPropertyName("posters")]
    public PostersDto Posters { get; set; }

    [JsonPropertyName("type")]
    public TypeDto? Type { get; set; }
    
    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; }

    [JsonPropertyName("season")]
    public SeasonDto Season { get; set; }

    [JsonPropertyName("ruDescription")]
    public string RuDescription { get; set; }

    [JsonPropertyName("in_favorites")]
    public long? InFavorites { get; set; }

    [JsonPropertyName("player")]
    public PlayerDto Player { get; set; }
}
