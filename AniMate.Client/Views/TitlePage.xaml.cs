﻿using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private readonly TitleViewModel _viewModel;

    private bool inLikes = false;

    public TitlePage(TitleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void OnWatchButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string hlsUrl)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"Title", _viewModel.Title }
            };

            await Shell.Current.GoToAsync(nameof(PlayerPage), navigationParameter);
        }
    }

    private void OnTextRecognizerTap(object sender, TappedEventArgs e)
    {
        _viewModel.ToggleDescription();
    }

    private async void LikeButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.LikesButtonClicked();
    }

    private async void WatchTogetherButtonClicked(object sender, EventArgs e)
    {
        if (!_viewModel.SharedWatchAwailable)
        {
            await DisplayAlert("Ошибка", "Совместный просмотр недоступен: нет серий", "Ок");
            return;
        }
        var navigationParameter = new Dictionary<string, object>
        {
            {"Title", _viewModel.Title},
            {"Url", _viewModel.Title.Player.Episodes.FirstOrDefault().Value.HlsUrls.Sd }
        };

        await Shell.Current.GoToAsync(nameof(SharedWatchingPage), navigationParameter);
    }

    private void ShareLinkButtonClicked(object sender, EventArgs e)
    {
        _viewModel.ShareTitleUrl();
    }

    private void StopButtonLoad(object sender, EventArgs e)
    {
        _viewModel.StopButtonLoad();
    }

    private void UpdateDescription(object sender, EventArgs e)
    {
        _viewModel.ToggleDescription();
    }
}