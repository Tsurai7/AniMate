using System;
using Microsoft.Maui.Controls;

namespace AniMate_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}