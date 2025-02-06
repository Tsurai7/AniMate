namespace AniMate_app.DTOs.Anime;

public class FranchiseDto
{
    public string? Id { get; init; }
    
    public string? Name { get; init; }
    
    public List<ReleaseDto>? Releases { get; init; }
}