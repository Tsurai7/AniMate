using AniMate_app.ViewModel;

namespace AniMate_app.View
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MainViewModel();
        }

        private void OnImageTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayerPage());            
        }

        private async void LoadContent(object sender, EventArgs e)
        {
             await viewModel.LoadContent();
        }

        private async void LoadMoreGenres(object sender, EventArgs e)
        {
            if (viewModel.AllGenresLoaded || !viewModel.IsLoaded)
                return;

            await viewModel.LoadTitlesByGenre(1);
        }
    }
}
