using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(Title), "Title")]
public partial class PlayerViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mediaUrl;

    private TitleDto _title;
    public TitleDto Title
    {
        get => _title;
        set
        {
            _title = value;
            MediaUrl = _title.Player.Episodes.FirstOrDefault().Value.HlsUrls.Sd;
            OnPropertyChanged(nameof(Title));
        }
    }

    [ObservableProperty]
    private bool _isFullScreen;

    private IScreenOrientationService _fullscreenService;

    public PlayerViewModel(IScreenOrientationService fullScreenService)
    {
        _fullscreenService = fullScreenService;
    }

    public void ToFullScreen()
    {
        _fullscreenService.AllowFullScreen();
    }

    public void RestoreOrientation()
    {
        _fullscreenService.RestrictFullScreen();
    }
}