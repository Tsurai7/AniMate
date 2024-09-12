using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(MediaUrl), "mediaurl")]
    public partial class PlayerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _mediaUrl;
    }
}