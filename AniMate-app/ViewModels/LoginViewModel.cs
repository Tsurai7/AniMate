using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniMate_app.Services.AuthService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        public readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }
    }
}
