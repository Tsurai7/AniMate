namespace AccountService.Domain.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfileImage { get; set; }
    public List<string> WatchedTitles { get; set; }
    public List<string> LikedTitles { get; set; }
}