using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private readonly TitleViewModel _viewModel;

    private bool isFullDescriptionOpen = false;

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
            await Shell.Current.GoToAsync(nameof(PlayerPage));
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
        var navigationParameter = new Dictionary<string, object>
        {
            {"Title", _viewModel.TitleDto},
            {"Url", _viewModel.TitleDto.Player.Episodes.FirstOrDefault().Value.HlsUrls.Sd }
        };
        
        await Shell.Current.GoToAsync("SharedWatchingPage", navigationParameter);
    }
}