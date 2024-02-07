using AniMate_app.ViewModels;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    public readonly GenreViewModel viewModel;

    public GenrePage(GenreViewModel genreViewModel)
    {
        InitializeComponent();

        BindingContext = viewModel = genreViewModel;
    }
}