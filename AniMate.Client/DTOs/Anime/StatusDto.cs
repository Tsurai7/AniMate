using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;
public record StatusDto
(
    [JsonProperty("string")]
    string? Status,

    [JsonProperty("code")]
    string? Code
);
