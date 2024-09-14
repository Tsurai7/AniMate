using System.Text.Json.Serialization;

namespace AniMate_app.DTOs.Anime;

public class PostersDto
{
    public PosterSize Small { get; init; }
    public PosterSize Medium { get; init; }
    public PosterSize Original { get; init; }
}
