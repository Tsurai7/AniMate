using AniMate_app.Services.AnilibriaService.Dtos;
using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Models
{
    public class Title
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("names")]
        public NamesDto? Names { get; set; }

        [JsonProperty("franchises")]
        public List<FranchiseDto>? Franchises { get; set; }

        [JsonProperty("status")]
        public StatusDto? Status { get; set; }

        [JsonProperty("posters")]
        public PostersDto Posters { get; set; }

        [JsonProperty("type")]
        public TypeDto? Type { get; set; }
        
        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("season")]
        public SeasonDto? Season { get; set; }

        [JsonProperty("description")]
        public string? RuDescription { get; set; }

        [JsonProperty("in_favorites")]
        public long InFavorites { get; set; }

        [JsonProperty("player")]
        public PlayerDto? Player { get; set; }
    }
}
