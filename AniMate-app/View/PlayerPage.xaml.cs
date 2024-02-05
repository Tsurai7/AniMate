using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModel;

namespace AniMate_app.View;

public partial class PlayerPage : ContentPage
{
    private PlayerViewModel viewModel;

    public PlayerPage(Title title)
    {
        InitializeComponent();

        BindingContext = viewModel = new PlayerViewModel(title);
    }

    private void OnWatchButtonClicked(object sender, EventArgs e)
    {
        mediaControl.IsVisible = true;
    }
}