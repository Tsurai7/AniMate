using System.Collections.ObjectModel;
using AniMate_app.Clients;
using AniMate_app.DTOs.Anime;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleDto), "Title")]
[QueryProperty(nameof(MediaUrl), "Url")]
public partial class SharedWatchingViewModel : ObservableObject
{
    [ObservableProperty]
    private TitleDto _titleDto;
    
    [ObservableProperty]
    private string _mediaUrl = string.Empty;

    [ObservableProperty]
    private string _roomCode = "123";
    
    public ObservableCollection<string> _chatMessages { get; set; }
    
    public readonly SharedWatchingClient _client;

    public SharedWatchingViewModel(SharedWatchingClient client)
    {
        _client = client;
    }
}