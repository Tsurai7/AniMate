using System;
using AniMate_app.Services;
using CommunityToolkit.Maui.Core.Primitives;
using Microsoft.Maui.Controls;

namespace AniMate_app.Views
{
    public partial class SharedWatchingPage : ContentPage
    {
        private readonly SharedWatchingService _signalRService;
        private bool _isSeeking;

        public SharedWatchingPage()
        {
            InitializeComponent();

            _signalRService = new SharedWatchingService();

            // Подписываемся на события SignalR
            _signalRService.OnSyncStateReceived += OnSyncStateReceived;
            _signalRService.OnMessageReceived += OnMessageReceived;
            _signalRService.OnError += OnError;
             _signalRService.JoinRoom("123");

            // Подписываемся на изменение позиции в mediaControl
            mediaControl.PositionChanged += MediaControl_PositionChanged;

            AppShell.SetNavBarIsVisible(this, true);
            AppShell.SetTabBarIsVisible(this, false);
        }

        private async void MediaControl_PositionChanged(object sender, MediaPositionChangedEventArgs e)
        {
            if (!_isSeeking)
            {
                // Отправляем текущее время на сервер, чтобы синхронизировать его с другими клиентами
                await _signalRService.Seek("sharedRoomLink", e.Position.TotalSeconds);
            }
        }

        private void OnSyncStateReceived(string url, double time, bool isPlaying)
        {
            if (mediaControl.Source?.ToString() != url)
            {
                mediaControl.Source = url;
            }
            if (isPlaying)
            {
                mediaControl.Play();
            }
            else
            {
                mediaControl.Pause();
            }
        }

        private void OnMessageReceived(string user, string message)
        {
            DisplayAlert("New Message", $"{user}: {message}", "OK");
        }

        private void OnError(string errorMessage)
        {
            DisplayAlert("Error", errorMessage, "OK");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            PauseVideo();
        }

        private void PauseVideo()
        {
            mediaControl.Pause();
            mediaControl.Handler?.DisconnectHandler();
        }
        
        private async void OnPlayButtonClicked(object sender, EventArgs e)
        {
            await _signalRService.Resume("123", mediaControl.Position.TotalSeconds);
            mediaControl.Play();
        }

        private async void OnPauseButtonClicked(object sender, EventArgs e)
        {
            await _signalRService.Pause("123", mediaControl.Position.TotalSeconds);
            mediaControl.Pause();
        }
    }
}
