using MediatR;

namespace Backend.Application.Handlers;

public class MyEmptyRequest : IRequest
{
}

public record ProfileResponse
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);