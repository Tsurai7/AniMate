using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public class FranchiseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("releases")]
        public List<ReleaseDto> Releases { get; set; }
    }

    public class ReleaseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("ordinal")]
        public string Ordinal { get; set; }

        [JsonProperty("names")]
        public NamesDto Names { get; set; }
    }
}
