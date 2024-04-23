using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Username), "Email")]
    public class ProfileViewModel : ObservableObject
    {
        private string _username;
        
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
    }
}
