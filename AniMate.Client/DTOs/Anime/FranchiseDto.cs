using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class FranchiseDto
{
    [JsonPropertyName("id")] string? Id { get; init; }
    [JsonPropertyName("name")] 
    public string? Name { get; init; }
    [JsonPropertyName("releases")]
    public List<ReleaseDto>? Releases { get; init; }
}