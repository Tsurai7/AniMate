using AniMate_app.DTOs.Anime;

namespace AniMate_app.Interfaces
{
    public interface IApplicationLinkService
    {
        Task ShareText(string text);
        string CreateTitleLink(TitleDto title);
        string CreateRoomLink(string roomId);
        string CreateRandomID();
    }
}
