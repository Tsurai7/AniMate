using Newtonsoft.Json;

namespace AniMate_app.Anilibria
{
    public record SearchDto
    {
        [JsonProperty("list")]
        public List<TitleRequestDto> Titles { get; set; }
    }
}
