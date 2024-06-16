using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public record PostersDto
(
    [JsonProperty("small")]
    PosterSize Small,

    [JsonProperty("medium")]
    PosterSize Medium,

    [JsonProperty("original")]
    PosterSize Original
);
