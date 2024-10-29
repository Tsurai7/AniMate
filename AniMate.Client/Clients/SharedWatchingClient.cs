using Microsoft.AspNetCore.SignalR.Client;

namespace AniMate_app.Clients;

public class SharedWatchingClient
{
    private readonly HubConnection _hubConnection;
    
#if DEBUG
    private const string HubUrl = "http://10.0.2.2:5002/sharedWatchingHub";
#else
    private const string HubUrl = "http://192.168.105.95:5002/sharedWatchingHub";
#endif
    
    public event Action<string, string, string> RoomCreated;
    public event Action<string, double, bool> SyncState;
    public event Action<string, double> Paused;
    public event Action<string, double> Resumed;
    public event Action<string, double> Seeked;
    public event Action<string> VideoUrlUpdated;
    public event Action<string> MessageReceived;

    public SharedWatchingClient()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();
        
        _hubConnection.On<string, string, string>("CreatedRoom", (link, titleCode, episodeUrl) =>
        {
            RoomCreated?.Invoke(link, titleCode, episodeUrl);
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

        _hubConnection.On<string, double, bool>("SyncStateForNewClient", (url, time, isPlaying) =>
        {
            VideoUrlUpdated?.Invoke(url);
            Seeked?.Invoke("Seeked", time);
            if (isPlaying)
                Resumed?.Invoke("Resumed", time);
            else
                Paused?.Invoke("Paused", time);
        });

        _hubConnection.On<string>("ReceiveMessage", (message) =>
        {
            MessageReceived?.Invoke(message);
        });
    }
    
    public async Task ConnectAsync()
    {
        try
        {
            await _hubConnection.StartAsync();
            Console.WriteLine("Connected to SignalR hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to hub: {ex.Message}");
        }
    }

    public async Task DisconnectAsync()
    {
        await _hubConnection.StopAsync();
    }

    public async Task CreateRoom(string link, string titleCode, string episodeUrl)
    {
        await _hubConnection.InvokeAsync("CreateRoom", link, titleCode, episodeUrl);
    }

    public async Task JoinRoom(string link)
    {
        await _hubConnection.InvokeAsync("JoinRoom", link);
    }

    public async Task Pause(string roomName, double currentTiming)
    {
        await _hubConnection.InvokeAsync("Pause", roomName, currentTiming);
    }

    public async Task Resume(string roomName, double currentTiming)
    {
        await _hubConnection.InvokeAsync("Resume", roomName, currentTiming);
    }

    public async Task Seek(string roomName, double newTime)
    {
        await _hubConnection.InvokeAsync("Seek", roomName, newTime);
    }

    public async Task UpdateVideoUrl(string roomName, string newVideoUrl)
    {
        await _hubConnection.InvokeAsync("UpdateVideoUrl", roomName, newVideoUrl);
    }

    public async Task SendMessage(string roomName, string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", roomName, message);
    }

    public async Task SyncStateForNewClient(string roomName)
    {
        await _hubConnection.InvokeAsync("SyncStateForNewClient", roomName);
    }
}