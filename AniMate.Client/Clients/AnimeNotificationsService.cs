using System.Net.WebSockets;

namespace AniMate_app.Clients;

public class AnimeNotificationsService
{
    private const string Url = "wss://api.anilibria.tv/v3/ws/";
    private readonly ClientWebSocket _clientWebSocket = new();
    
    private static async Task ConnectAsync()
    {
        using var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri(Url), CancellationToken.None);


        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
    }
}