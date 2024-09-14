using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace AniMate_app.Clients;

public class SharedWatchingClient
{
    private HubConnection _hubConnection;
    
    private const string HubUrl = "http://192.168.8.7:5002/sharedWatchingHub";

    public SharedWatchingClient()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task JoinRoom()
    {
        
    }
}