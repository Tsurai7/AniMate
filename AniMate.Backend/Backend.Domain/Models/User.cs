namespace Backend.Domain.Models;

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImageUrl { get; set; }
    public Stack<string> LikedTitles { get; set; }
    public Stack<string> WatchedTitles { get; set; }
}