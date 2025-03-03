using System.Text.Json.Serialization;

namespace Backend.Domain.Models.Anime; 

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
    
    [JsonPropertyName("updated")]
    public int Updated { get; set; }  // Last new release in the title
    
    [JsonPropertyName("last_change")]
    public int LastChange { get; set; } // Last title changes (description, announce, etc.)
    
    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; }

    [JsonPropertyName("season")]
    public SeasonDto Season { get; set; }

    [JsonPropertyName("description")]
    public string RuDescription { get; set; }
    [JsonPropertyName("in_favorites")]
    public long? InFavorites { get; set; }

    [JsonPropertyName("player")]
    public PlayerDto Player { get; set; }
}
