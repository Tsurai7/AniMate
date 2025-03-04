using System.Text.Json.Serialization;
using Backend.Domain.Models.Anime;

namespace Backend.API.Controllers.Models.Titles;

public class Title
{
    public int Id { get; init; }
    public string Code { get; init; }
    public NamesDto Names { get; init; }
    public List<FranchiseDto>? Franchises { get; init; }
    public StatusDto Status { get; init; }
    public Posters Posters { get; init; }
    public TypeDto? Type { get; init; }
    public List<string> Genres { get; init; }
    public SeasonDto Season { get; init; }
    public string RuDescription { get; init; }
    public Player Player { get; init; }
}

public class Posters
{
    public PosterSize Small { get; init; }
    public PosterSize Medium { get; init; }
    public PosterSize Original { get; init; }
}

public class PosterSize
{
    public string Url { get; set; }
}

public class Player
{
    public string? AlternativePlayer { get; init; }
    public string Host { get; init; }
    [JsonPropertyName("list")]
    public Dictionary<string, EpisodeDto> Episodes { get; init; }
}

public class EpisodeDto
{
    public int Episode { get; init; }
    public string? Name { get; init; }
    public Hls HlsUrls { get; init; }
}

public class Hls
{
    public string Fhd { get; init; }
    public string Hd { get; init; }
    public string Sd { get; init; }
}