using System;
using Microsoft.Maui.Controls;

namespace AniMate_app.Views
{
    public partial class SharedWatchingPage : ContentPage
    {
        public SharedWatchingPage()
        {
            InitializeComponent();
        }

        private void OnEpisodeSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                string selectedEpisode = (string)picker.ItemsSource[selectedIndex];
                // Здесь вы можете добавить логику для загрузки выбранной серии
                // Например, изменить Source у MediaControl
                // MediaControl.Source = $"https://cache.libria.fun/videos/media/ts/9000/{selectedIndex + 1}/720/episode.m3u8";
            }
        }

        private void OnNextEpisodeSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                // Здесь вы можете добавить логику для перехода к следующей серии
                int currentEpisode = EpisodePicker.SelectedIndex;
                int nextEpisode = currentEpisode + selectedIndex + 1;
                
                if (nextEpisode < EpisodePicker.Items.Count)
                {
                    EpisodePicker.SelectedIndex = nextEpisode;
                    // Обновите источник видео здесь
                    // MediaControl.Source = $"https://cache.libria.fun/videos/media/ts/9000/{nextEpisode + 1}/720/episode.m3u8";
                }
            }
        }
    }
}