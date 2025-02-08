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
            var navigationParameter = new Dictionary<string, object>
            {
                {"mediaurl", _viewModel.Title.Player.Episodes.FirstOrDefault().Value.HlsUrls.Sd }
            };

            await Shell.Current.GoToAsync(nameof(PlayerPage), navigationParameter);
        }
    }

    private void OnTextRecognizerTap(object sender, TappedEventArgs e)
    {
        UpdateTitleDescription();

        isFullDescriptionOpen = !isFullDescriptionOpen;
    }

    private void UpdateTitleDescription()
    {
        FormattedString formattedString = new();

        formattedString.Spans.Add(new Span
        {
            Text = isFullDescriptionOpen ? _viewModel.ShortDescription : _viewModel.Title.RuDescription,
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
    }

    private async void LikeButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.LikesButtonClicked();
    }

    private async void WatchTogetherButtonClicked(object sender, EventArgs e)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"Title", _viewModel.Title},
            {"Url", _viewModel.Title.Player.Episodes.FirstOrDefault().Value.HlsUrls.Sd }
        };
        
        await Shell.Current.GoToAsync("SharedWatchingPage", navigationParameter);
    }
}