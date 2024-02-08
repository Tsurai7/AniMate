using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;

        private bool isOpeningPlayer = false;

        private bool isFirstLoad = true;

        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = mainViewModel;
        }

        private async void LoadContent(object sender, EventArgs e)
        {
            if(isFirstLoad)
                await viewModel.LoadContent();
        }

        private async void TitleSelected(object sender, SelectionChangedEventArgs e)
        {
            if (isOpeningPlayer)
            {
                (sender as CollectionView).SelectedItem = null;

                return;
            }   

            isOpeningPlayer = true;

            var collectionView = sender as CollectionView;

            await Navigation.PushAsync(new TitlePage(collectionView.SelectedItem as Title));

            collectionView.SelectedItem = null;

            isOpeningPlayer = false;
        }

        private void ContentPage_Unloaded(object sender, EventArgs e)
        {
            isFirstLoad = false;
        }

        private async void OnGenreLabelTapped(object sender, TappedEventArgs e)
        {
            var tappedLabel = (Label)sender;

            string genreName = tappedLabel.Text;

            var titles = viewModel.GenreList.FirstOrDefault(t => t.GenreName == genreName).Titles;

            await Navigation.PushAsync(new GenrePage(genreName, titles, viewModel._anilibriaService));
        }
    }
}
    