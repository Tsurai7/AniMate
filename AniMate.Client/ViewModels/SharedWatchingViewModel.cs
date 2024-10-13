using System.Collections.ObjectModel;
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
    
    public ObservableCollection<string> _chatMessages { get; set; }
}