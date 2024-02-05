using Newtonsoft.Json;

namespace AniMate_backend.Models
{
    public class TitlesInfo
    {
        [JsonProperty("list")]
        public List<Title>? Titles { get; set; }
    }
}
