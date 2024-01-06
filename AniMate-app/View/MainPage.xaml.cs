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
    }
}
