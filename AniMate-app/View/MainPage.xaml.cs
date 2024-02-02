using AniMate_app.Anilibria;
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

        private async void TitleSelected(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count.Equals(0))
                return;

            var collectionView = sender as CollectionView;

            await Navigation.PushAsync(new PlayerPage(collectionView.SelectedItem as TitleRequestDto));

            collectionView.SelectedItem = null;
        }
    }
}
