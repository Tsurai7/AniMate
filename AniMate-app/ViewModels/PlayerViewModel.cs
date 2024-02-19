using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(MediaUrl), "mediaurl")]
    public partial class PlayerViewModel : ObservableObject
    {
        private string _mediaUrl;

        public string MediaUrl {
            get => _mediaUrl;
            set
            {
                _mediaUrl = value;
                OnPropertyChanged(nameof(MediaUrl));
            }
            }

        public PlayerViewModel() { }
    }
}
