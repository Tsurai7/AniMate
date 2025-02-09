using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(MediaUrl), "mediaurl")]
public partial class PlayerViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mediaUrl;

    [ObservableProperty]
    private bool _isFullScreen;

    private IFullScreenService _fullscreenService;

    public PlayerViewModel(IFullScreenService fullScreenService)
    {
        _fullscreenService = fullScreenService;
    }

    public void ToFullScreen()
    {
        _fullscreenService.EnterFullScreen();
    }

    public void RestoreOrientation()
    {
        _fullscreenService.RestoreOriginal();
    }
}