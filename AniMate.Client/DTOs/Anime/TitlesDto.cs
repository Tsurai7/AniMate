using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public record TitlesInfo(
    [JsonProperty("list")] List<TitleDto> Titles
);
