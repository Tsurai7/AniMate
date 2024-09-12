using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class PosterSize
{
    private const string BaseAdress = "https://www.anilibria.tv";

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonConstructor]
    public PosterSize(string url)
    {
        Url = $"{BaseAdress}{url}";
    }
}