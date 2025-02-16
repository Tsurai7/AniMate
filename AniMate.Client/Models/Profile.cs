namespace AniMate_app.Models;

public class Profile
{
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LasName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    
    public List<string> LikedTitles { get; set; } = [];
    public List<string> WatchedTitles { get; set; } = [];
}