using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public record TypeDto
(
    [JsonProperty("full_string")]
    string? FullInfo,

    [JsonProperty("string")]
    string? Type,

    [JsonProperty("series")]
    string? Series,

    [JsonProperty("length")]
    string? Length
);
