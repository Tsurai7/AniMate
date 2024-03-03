namespace AniMate_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);

            await Shell.Current.GoToAsync($"titlepage?TitleCode={uri.Segments[1]}");
        }
    }
}