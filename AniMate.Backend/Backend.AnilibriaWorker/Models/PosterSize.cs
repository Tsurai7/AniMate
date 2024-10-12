using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class PosterSize
{
    private const string BaseAdress = "https://www.anilibria.tv";
    public string Url { get; set; }

    [JsonConstructor]
    public PosterSize(string url)
    {
        Url = $"{BaseAdress}{url}";
    }
}