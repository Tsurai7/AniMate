using System.Collections.ObjectModel;
using AniMate_app.Clients;
using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(Title), "Title")]
[QueryProperty(nameof(MediaUrl), "Url")]
[QueryProperty(nameof(RoomId), "RoomId")]
public partial class SharedWatchingViewModel : ObservableObject
{
    [ObservableProperty]
    private TitleDto _title;

    [ObservableProperty] 
    private string _mediaUrl = string.Empty;

    [ObservableProperty]
    private string _roomId = string.Empty;
    
    public ObservableCollection<string> _chatMessages { get; set; } = new();

    public SharedWatchingClient _client;

    private readonly IApplicationLinkService _linkService;

    public bool HasConnection => _client.HasConnection;

    public SharedWatchingViewModel(IApplicationLinkService linkService)
    {
        _linkService = linkService;

        _client = new();
    }

    public async void ShareRoomLink()
    {
        var link = _linkService.CreateRoomLink(RoomId);

        await _linkService.ShareText(link);
    }

    public async Task Connect()
    {
        await _client.ConnectAsync();

        if (string.IsNullOrEmpty(RoomId))
        {
            await _client.CreateRoom(Title.Code, MediaUrl);

            return;
        }

        await _client.JoinRoom(RoomId);

        //await _client.SyncStateForNewClient(RoomId);
    }

    public async Task ChangeVideoUrl(string episodeUrl)
    {
        await _client.UpdateVideoUrl(RoomId, episodeUrl);
    }

    public async Task SendMessage(string text)
    {
        await _client.SendMessage(RoomId, text);
    }

    public void AddMessage(string message)
    {
        _chatMessages.Add(message);
    }

    public async Task Disconnect()
    {
        await _client.DisconnectAsync();
    }

    public async Task Seek(double totalSeconds)
    {
        await _client.Seek(RoomId, totalSeconds);
    }

    public async Task Resume(string roomId, double totalSeconds)
    {
        await _client.Resume(roomId, totalSeconds);
    }

    public async Task Pause(string roomId, double totalSeconds)
    {
        await _client.Pause(roomId, totalSeconds);
    }
}