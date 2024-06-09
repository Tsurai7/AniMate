using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public class StatusDto
    {
        [JsonProperty("string")]
        public string? Status { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }
    }
}
