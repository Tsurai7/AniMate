using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniMate_app.ViewModels
{
    internal class PlayerViewModel
    {
        public string MediaUrl { get; set; }

        public PlayerViewModel(string mediaUrl)
        {
            MediaUrl = mediaUrl;
        }
    }
}
