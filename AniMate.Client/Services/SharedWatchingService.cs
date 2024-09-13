using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace AniMate_app.Services;

public class SharedWatchingService
{
    private HubConnection _hubConnection;

    public event Action<string, string> OnMessageReceived;
    public event Action<string, double, bool> OnSyncStateReceived;
    public event Action<string> OnError;
    private const string HubUrl = "192.168.8.7/sharedWatchingHub";

    public async Task ConnectAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();

        // Подписываемся на события от сервера
        _hubConnection.On<string, double, bool>("SyncState", (url, time, isPlaying) =>
        {
            OnSyncStateReceived?.Invoke(url, time, isPlaying);
        });

        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            OnMessageReceived?.Invoke(user, message);
        });

        _hubConnection.On<string>("Error", (errorMessage) =>
        {
            OnError?.Invoke(errorMessage);
        });

        // Подключаемся к серверу
        await _hubConnection.StartAsync();
    }

    public async Task SendMessage(string roomName, string message)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.InvokeAsync("SendMessage", roomName, message);
        }
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
}