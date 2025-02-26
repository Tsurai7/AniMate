using Microsoft.AspNetCore.SignalR.Client;

namespace AniMate_app.Clients;

public class SharedWatchingClient
{
    private readonly HubConnection _hubConnection;
    
#if DEBUG
    private const string HubUrl = "http://10.0.2.2:8080/sharedWatchingHub";
#else
    private const string HubUrl = "https://tsurai7-animate-910d.twc1.net/sharedWatchingHub";
#endif

    public bool HasConnection => _hubConnection != null && _hubConnection.State.Equals(HubConnectionState.Connected);

    public event Action<string> RoomCreated;
    public event Action<string, double, bool> SyncState;
    public event Action<string, double> Paused;
    public event Action<string, double> Resumed;
    public event Action<string, double> Seeked;
    public event Action<string> VideoUrlUpdated;
    public event Action<string> MessageReceived;
    public event Action<string> Error;

    public SharedWatchingClient()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();
        
        _hubConnection.On<string>("CreatedRoom", (roomId) =>
        {
            RoomCreated?.Invoke(roomId);
        });

        _hubConnection.On<string, double, bool>("SyncState", (url, timing, isPlaying) =>
        {
            SyncState?.Invoke(url, timing, isPlaying);
        });

        _hubConnection.On<double>("Pause", (timing) =>
        {
            Paused?.Invoke("Paused", timing);
        });

        _hubConnection.On<double>("Resume", (timing) =>
        {
            Resumed?.Invoke("Resumed", timing);
        });

        _hubConnection.On<double>("Seek", (newTime) =>
        {
            Seeked?.Invoke("Seeked", newTime);
        });

        _hubConnection.On<string>("VideoUrlUpdated", (newUrl) =>
        {
            VideoUrlUpdated?.Invoke(newUrl);
        });

        _hubConnection.On<string>("ReceiveMessage", (message) =>
        {
            MessageReceived?.Invoke(message);
        });
        
        _hubConnection.On<string>("Error", (message) =>
        {
            Error?.Invoke(message);
        });
    }
    
    public async Task ConnectAsync()
    {
        try
        {
            if (_hubConnection is null)
                return;

            await _hubConnection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to hub: {ex.Message}");
        }
    }

    public async Task DisconnectAsync()
    {
        if (!HasConnection)
            return;

        await _hubConnection.StopAsync();
    }

    public async Task CreateRoom(string titleCode, string episodeUrl)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("CreateRoom", titleCode, episodeUrl);
    }

    public async Task JoinRoom(string link)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("JoinRoom", link);
    }

    public async Task Pause(string roomName, double currentTiming)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("Pause", roomName, currentTiming);
    }

    public async Task Resume(string roomName, double currentTiming)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("Resume", roomName, currentTiming);
    }

    public async Task Seek(string roomName, double newTime)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("Seek", roomName, newTime);
    }

    public async Task UpdateVideoUrl(string roomName, string newVideoUrl)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("UpdateVideoUrl", roomName, newVideoUrl);
    }

    public async Task SendMessage(string roomName, string message)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("SendMessage", roomName, message);
    }

    public async Task SyncStateForNewClient(string roomName)
    {
        if (!HasConnection)
            return;

        await _hubConnection.InvokeAsync("SyncStateForNewClient", roomName);
    }
}
