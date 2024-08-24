using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public record ReleaseDto
(
    [JsonProperty("id")]
    string? Id,

    [JsonProperty("code")]
    string? Code,

    [JsonProperty("ordinal")]
    string? Ordinal,

    [JsonProperty("names")]
    NamesDto? Names
);