using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;
public record PlayerDto
(
    [JsonProperty("alternative_player")]
    string? AlternativePlayer,

    [JsonProperty("host")]
    string Host,
    
    [JsonProperty("list")]
    Dictionary<string, EpisodeDto> Episodes
);
