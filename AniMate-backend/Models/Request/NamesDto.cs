using Newtonsoft.Json;

namespace AniMate_backend.Models.TitleInfo
{
    public record NamesDto
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }

        [JsonProperty("en")]
        public string En { get; set; }
    }
}
