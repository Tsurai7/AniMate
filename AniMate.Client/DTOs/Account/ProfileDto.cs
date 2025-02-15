namespace AniMate_app.DTOs.Account;

public record ProfileDto
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);