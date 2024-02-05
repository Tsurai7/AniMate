using Newtonsoft.Json;

namespace AniMate_app.Services.AnilibriaService.Dtos
{
    public class PostersDto
    {
        [JsonProperty("small")]
        public PosterSize Small { get; set; }

        [JsonProperty("medium")]
        public PosterSize Medium { get; set; }

        [JsonProperty("original")]
        public PosterSize Original { get; set; }
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
