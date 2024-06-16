using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;
public record SeasonDto
(
    [JsonProperty("string")]
    string? Season,

    [JsonProperty("code")]
    string? Code,

    [JsonProperty("year")]
    string? Year
);

