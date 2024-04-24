using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniMate_app.Services.AuthService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public class SignInViewModel : ObservableObject
    {
        public readonly AuthService AuthService;

        public SignInViewModel(AuthService authService)
        {
            AuthService = authService;
        }
    }
}
