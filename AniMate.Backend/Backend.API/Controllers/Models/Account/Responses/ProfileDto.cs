namespace Backend.API.Controllers.Models.Account.Responses;

public record ProfileDto
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);