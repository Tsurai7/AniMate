using System;
using AniMate_app.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private readonly TitleViewModel _viewModel;

    private bool isFullDescriptionOpen = false;

    private bool inLikes = false;

    public TitlePage(TitleViewModel titleViewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = titleViewModel;

        inLikes = _viewModel.IsTitleInLikes;
    }

    private async void OnWatchButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string hlsUrl)
        {
            await _viewModel.WatchButtonClicked();
            await Shell.Current.GoToAsync($"PlayerPage?mediaurl={hlsUrl}");

        }
    }

    private void OnTextRecognizerTap(object sender, TappedEventArgs e)
    {
        FormattedString formattedString = new();

        formattedString.Spans.Add(new Span
        {
            Text = isFullDescriptionOpen ? _viewModel.ShortDescription : _viewModel.TitleDto.RuDescription,
        });

        if (isFullDescriptionOpen)
        {
            formattedString.Spans.Add(new Span
            {
                Text = "... ещё",
                TextColor = Colors.Grey,
            });
        }

        descriptionLabel.FormattedText = formattedString;

        isFullDescriptionOpen = !isFullDescriptionOpen;
    }

    private async void LikeButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.LikesButtonClicked();
    }

    private async void WatchTogetherButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SharedWatchingPage());
    }
}