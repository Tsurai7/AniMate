using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private readonly TitleViewModel _viewModel;

    private bool isFullDescriptionOpen = false;

    public TitlePage(TitleViewModel titleViewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = titleViewModel;
    }

    private async void OnWatchButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string hlsUrl)
            await Shell.Current.GoToAsync($"playerpage?mediaurl={hlsUrl}");
    }

    private void OnTextRecognizerTap(object sender, TappedEventArgs e)
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

        isFullDescriptionOpen = !isFullDescriptionOpen;
    }
}