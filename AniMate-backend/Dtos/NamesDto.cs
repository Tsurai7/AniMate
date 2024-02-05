using Newtonsoft.Json;

namespace AniMate_backend.Dtos
{
    public record NamesDto
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }

        [JsonProperty("en")]
        public string En { get; set; }
    }
}
