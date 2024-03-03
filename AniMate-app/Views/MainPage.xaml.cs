using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModels;

namespace AniMate_app.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;

        private bool _isOpeningPlayer = false;

        private bool _isFirstLoad = true;

        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = mainViewModel;
        }

        private async void LoadContent(object sender, EventArgs e)
        {
            if(_isFirstLoad)
            {
                await viewModel.LoadContent();

                _isFirstLoad = false;
            }
                
        }

        private async void TitleSelected(object sender, SelectionChangedEventArgs e)
        {
            var collection = sender as CollectionView;
            if (_isOpeningPlayer)
            {
                collection.SelectedItem = null;

                return;
            }   

            _isOpeningPlayer = true;

            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", e.CurrentSelection}
            };

            await Shell.Current.GoToAsync($"titlepage", navigationParameter);

            collection.SelectedItem = null;

            _isOpeningPlayer = false;
        }

        private async void OnGenreTapped(object sender, TappedEventArgs e)
        {
            GenreCollection genreCollection = e.Parameter as GenreCollection;

            var navigationParameter = new Dictionary<string, object>
            {
                {"GenreName", genreCollection.GenreName},
                {"AnilibriaService",  viewModel._anilibriaService}
            };

            await Shell.Current.GoToAsync($"genrepage", navigationParameter);
        }
    }
}
    