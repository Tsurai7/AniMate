using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Models
{
    public class TitlesInfo
    {
        [JsonProperty("list")]
        public List<Title> Titles { get; set; }
    }
}
