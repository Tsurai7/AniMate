using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;
public record NamesDto
(
    [JsonProperty("ru")]
    string Ru,

    [JsonProperty("en")]
    string En
);

