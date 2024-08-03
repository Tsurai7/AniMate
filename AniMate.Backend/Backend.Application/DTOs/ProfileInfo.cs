namespace Backend.Application.DTOs;

public record ProfileInfo
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);