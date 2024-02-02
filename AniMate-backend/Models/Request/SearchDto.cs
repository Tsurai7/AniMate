using Newtonsoft.Json;

namespace AniMate_backend.Models.TitleInfo
{
    public record SearchDto
    {
        [JsonProperty("list")]
        public List<TitleRequestDto> Titles { get; set; }
    }
}
