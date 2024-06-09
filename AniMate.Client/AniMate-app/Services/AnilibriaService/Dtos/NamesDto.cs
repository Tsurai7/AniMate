using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public record NamesDto
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }

        [JsonProperty("en")]
        public string En { get; set; }
    }
}
