using System.Text.Json.Serialization;

namespace Adapter.Domain.Models.Responses;

public class HlsDto
{
    private const string BaseAdress = "https://cache.libria.fun";

    [JsonConstructor]
    public HlsDto(string fhd, string hd, string sd)
    {
        Fhd = $"{BaseAdress}{fhd}";
        Hd = $"{BaseAdress}{hd}";
        Sd = $"{BaseAdress}{sd}";
    }
    
    public string Fhd { get; init; }
    
    public string Hd { get; init; }
    
    public string Sd { get; init; }
}