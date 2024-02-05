using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public class TypeDto
    {
        [JsonProperty("full_string")]
        public string FullInfo { get; set; }

        [JsonProperty("string")]
        public string Type { get; set; }

        [JsonProperty("series")]
        public string Series { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }
    }
}
