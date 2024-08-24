using Newtonsoft.Json;

namespace AniMate_app.DTOs.Anime;

public record HlsDto
{
    private const string BaseAdress = "https://cache.libria.fun";

    [JsonConstructor]
    public HlsDto(string fhd, string hd, string sd)
    {
        Fhd = $"{BaseAdress}{fhd}";
        Hd = $"{BaseAdress}{hd}";
        Sd = $"{BaseAdress}{sd}";
    }

    [JsonProperty("fhd")]
    public string Fhd { get; set; }

    [JsonProperty("hd")]
    public string Hd { get; set; }

    [JsonProperty("sd")]
    public string Sd { get; set; }
}