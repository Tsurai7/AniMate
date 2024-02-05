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
    void OnEntryCompleted(object sender, EventArgs e)
    {
        viewModel.FindTitles(entry.Text);
    }
}