using System.Text.Json.Serialization;

namespace Backend.AnilibriaWorker.Models; 

public class HlsDto
{
    private const string BaseAddress = "https://cache.libria.fun";

    [JsonConstructor]
    public HlsDto(string fhd, string hd, string sd)
    {
        Fhd = $"{BaseAddress}{fhd}";
        Hd = $"{BaseAddress}{hd}";
        Sd = $"{BaseAddress}{sd}";
    }

    [JsonPropertyName("fhd")]
    public string Fhd { get; init; }

    [JsonPropertyName("hd")]
    public string Hd { get; init; }

    [JsonPropertyName("sd")]
    public string Sd { get; init; }
}