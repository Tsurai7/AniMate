namespace Backend.Domain.Models;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImageUrl { get; set; }
    public List<string> LikedTitles { get; set; }
    public List<string> WatchedTitles { get; set; }
}