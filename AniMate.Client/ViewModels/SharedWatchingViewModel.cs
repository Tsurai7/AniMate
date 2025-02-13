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

        //_client.SyncStateForNewClient(RoomId);
    }

    public void ChangeVideoUrl(string episodeUrl)
    {
        _client.UpdateVideoUrl(RoomId, episodeUrl);
    }

    public void SendMessage(string text)
    {
        _client.SendMessage(RoomId, text);
    }

    public void AddMessage(string message)
    {
        _chatMessages.Add(message);
    }

    public async Task Disconnect()
    {
        await _client.DisconnectAsync();
    }

    public void Seek(double totalSeconds)
    {
        _client.Seek(RoomId, totalSeconds);
    }

    public void Resume(string roomId, double totalSeconds)
    {
        _client.Resume(roomId, totalSeconds);
    }

    public void Pause(string roomId, double totalSeconds)
    {
        _client.Pause(roomId, totalSeconds);
    }
}