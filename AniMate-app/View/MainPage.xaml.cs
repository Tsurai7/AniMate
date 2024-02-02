using AniMate_app.ViewModel;

namespace AniMate_app.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }

        private void OnImageTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayerPage());            
        }

        private async void LoadContent(object sender, EventArgs e)
        {
            var context = BindingContext as MainViewModel;

            await context.LoadContent();
        }

        private void ScrollToGenre(object sender, TappedEventArgs e)
        {
            //string genre = (sender as Label).Text;

            //var genreId = (BindingContext as MainViewModel).Genres.IndexOf(genre);

            GenreList.ScrollTo(5);
        }
    }
}
