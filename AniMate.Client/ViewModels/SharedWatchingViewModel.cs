using System.Collections.ObjectModel;
using AniMate_app.Clients;
using AniMate_app.DTOs.Anime;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleDto), "Title")]
[QueryProperty(nameof(MediaUrl), "Url")]
[QueryProperty(nameof(RoomId), "RoomId")]
public partial class SharedWatchingViewModel : ObservableObject
{
    [ObservableProperty]
    private TitleDto _titleDto;

    [ObservableProperty]
    private string _mediaUrl = string.Empty;

    private string _roomId = string.Empty;

    public ObservableCollection<string> _chatMessages { get; set; } = new();

    public SharedWatchingClient _client = new();

    public string RoomId
    {
        get => _roomId;
        set
        {
            _roomId = value;
        }
    }
}