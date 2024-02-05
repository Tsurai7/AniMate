using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Models
{
    public class Episode
    {
        [JsonProperty("hls")]
        public Hls? HlsUrls { get; set; }
    }

    public record Hls
    {
        [JsonProperty("fhd")]
        public string? Fhd { get; set; }

        [JsonProperty("hd")]
        public string? Hd { get; set; }

        [JsonProperty("sd")]
        public string? Sd { get; set; }
    }
}
