using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Backend.Domain.Models;

public class User
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string ProfileImage { get; set; }
    public List<string> WatchedTitles { get; set; }
    public List<string> LikedTitles { get; set; }

    public User( string userName, string email, string passwordHash, string profileImage, List<string> watchedTitles, List<string> likedTitles)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        ProfileImage = profileImage;
        WatchedTitles = watchedTitles;
        LikedTitles = likedTitles;
    }
}