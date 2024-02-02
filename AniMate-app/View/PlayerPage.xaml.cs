namespace AniMate_app.View;

public partial class PlayerPage : ContentPage
{
    public PlayerPage()
	{
        InitializeComponent();
	}

    private void OnWatchButtonClicked(object sender, EventArgs e)
    {
        mediaControl.IsVisible = true;
    }
}