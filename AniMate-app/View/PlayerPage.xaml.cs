using AniMate_app.Anilibria;
using AniMate_app.ViewModel;

namespace AniMate_app.View;

public partial class PlayerPage : ContentPage
{
    private PlayerViewModel viewModel;

    public PlayerPage(TitleRequestDto title)
    {
        BindingContext = viewModel = new PlayerViewModel(title);

        InitializeComponent();
    }

    private void OnWatchButtonClicked(object sender, EventArgs e)
    {
        mediaControl.IsVisible = true;
    }
}