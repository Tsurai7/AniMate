namespace AniMate_app.View;
using AniMate_app.ViewModel;
public partial class SearchPage : ContentPage
{
    private readonly SearchViewModel viewModel;

    public SearchPage(SearchViewModel searchViewModel)
	{
        InitializeComponent();

        BindingContext = viewModel = searchViewModel;
    }

    private async void OnEntryCompleted(object sender, EventArgs e)
    {
        await viewModel.FindTitles(entry.Text);
    }
}