ï»¿using AniMate_app.Services.AnilibriaService.Models;
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
    }
}