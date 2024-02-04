using Newtonsoft.Json;

namespace AniMate_app.Anilibria
{
    public class TitleRequestDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("names")]
        public NamesDto Names { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("player")]
        public PlayerDto Player { get; set; }

        [JsonProperty("posters")]
        public Posters Posters { get; set; }
    }

    public class Posters
    {
        [JsonProperty("small")]
        public PosterSize Small { get; set; }
    }

    public class PosterSize
    {
        private const string BaseAdress = "https://www.anilibria.tv";

        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonConstructor]
        public PosterSize(string url)
        {
            Url = $"{BaseAdress}{url}";
        }
    }
}
