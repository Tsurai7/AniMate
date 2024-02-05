using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public class SeasonDto
    {
        [JsonProperty("string")]
        public string Season { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }
    }
}
