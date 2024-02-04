using Newtonsoft.Json;

namespace AniMate_backend.Dtos
{
    public class StatusDto
    {
        [JsonProperty("string")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
