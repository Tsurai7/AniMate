using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public class FranchiseDto
(
    [JsonProperty("id")]
    string? Id,

    [JsonProperty("name")]
    string? Name,

    [JsonProperty("releases")]
    List<ReleaseDto>? Releases
);