using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace AniMate_app.Views;

public partial class TitlePage : ContentPage
{
    private TitleViewModel viewModel;

    public TitlePage(Title title)
    {
        InitializeComponent();

        BindingContext = viewModel = new TitleViewModel(title);

        GenerateButtons();
    }

    private void OnWatchButtonClicked(object sender, EventArgs e)
    {
        //mediaControl.IsVisible = true;
    }

    private void GenerateButtons()
    {
        StackLayout rowStackLayout = null;

        for (int i = 1; i <= viewModel.Title.Player.Episodes.Count; i++)
        {
            string episodeIndex = i.ToString();

            Button button = new()
            {
                Text = $"Серия {i}",
                BackgroundColor = Color.FromRgb(169, 169, 169),
                Margin = new Thickness(5, 5, 5, 5)
            };
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new PlayerPage(viewModel.Title.Player.Episodes[episodeIndex].HlsUrls.Sd));
            };

            if (i % 4 == 1)
            {
                rowStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };

                ButtonContainer.Children.Add(rowStackLayout);         
            }

            rowStackLayout.Children.Add(button);
        }
    }
}