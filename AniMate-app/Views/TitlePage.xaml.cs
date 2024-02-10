using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private TitleViewModel viewModel;

    private bool isFullDescriptionOpen = false;

    public TitlePage(Title title)
    {
        InitializeComponent();

        BindingContext = viewModel = new TitleViewModel(title);
    }

    private async void OnWatchButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string hlsUrl)
        {
            await Navigation.PushAsync(new PlayerPage(hlsUrl));
        }
        
    }

    private void OnTextRecognizerTap(object sender, EventArgs e)
    {
        if (isFullDescriptionOpen)
        {
            FormattedString formattedString = new FormattedString();

            formattedString.Spans.Add(new Span
            {
                Text = viewModel.ShortDescription,
                
            });
            formattedString.Spans.Add(new Span
            {
                Text = "... ещё",
                TextColor = Colors.Grey,
                
            });
            descriptionLabel.FormattedText = formattedString;
            isFullDescriptionOpen= false;
        }
        else
        {
            FormattedString formattedString = new FormattedString();

            formattedString.Spans.Add(new Span
            {
                Text = viewModel.Title.RuDescription,
                TextColor = Colors.Black,
                FontSize = 15,

            }); ;
            descriptionLabel.FormattedText = formattedString;
            isFullDescriptionOpen= true;
        }
        

    }
}