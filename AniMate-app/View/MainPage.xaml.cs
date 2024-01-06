using AniMate_app.Model;

namespace AniMate_app.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Create an instance of MainViewModel and set it as the BindingContext

            BindingContext = new ViewModel.MainViewModel();
        }

        private void OnImageTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayerPage());            
        }
    }
}
