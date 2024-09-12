using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class HlsDto
{
    private const string BaseAdress = "https://cache.libria.fun";

    [Newtonsoft.Json.JsonConstructor]
    public HlsDto(string fhd, string hd, string sd)
    {
        Fhd = $"{BaseAdress}{fhd}";
        Hd = $"{BaseAdress}{hd}";
        Sd = $"{BaseAdress}{sd}";
    }

    [JsonPropertyName("fhd")]
    public string Fhd { get; init; }

    [JsonPropertyName("hd")]
    public string Hd { get; init; }

    [JsonPropertyName("sd")]
    public string Sd { get; init; }
}