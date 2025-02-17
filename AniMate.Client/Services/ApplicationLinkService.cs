using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;

namespace AniMate_app.Services;

internal class ApplicationLinkService : IApplicationLinkService
{
    private const string APP_PROVIDER = "https://animate/";

    private const string TITLE_PATH = "anime/";

    private const string ROOM_PATH = "room/";

    public string CreateRandomID()
    {
        return new Guid().ToString();
    }

    public string CreateRoomLink(string roomId)
    {
        return $"{APP_PROVIDER}{ROOM_PATH}{roomId}";
    }

    public string CreateTitleLink(TitleDto title)
    {
        return $"{APP_PROVIDER}{TITLE_PATH}{title.Code}";
    }

    public async Task ShareText(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Поделиться ссылкой"
        });
    }
}