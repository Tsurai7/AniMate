using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.ViewModel;

namespace AniMate_app.View
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

            await Navigation.PushAsync(new PlayerPage(collectionView.SelectedItem as Title));

            collectionView.SelectedItem = null;

            isOpeningPlayer = false;
        }

        private void ContentPage_Unloaded(object sender, EventArgs e)
        {
            isFirstLoad = false;
        }
    }
}
