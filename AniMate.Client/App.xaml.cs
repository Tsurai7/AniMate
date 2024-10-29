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

            var action = uri.Segments[1];

            if (action.Equals("anime/"))
            {
                await Shell.Current.GoToAsync($"TitlePage?TitleCode={uri.Segments[2]}");
            }
            else if (action.Equals("room/"))
            {
                await Shell.Current.GoToAsync($"SharedWatchingPage?RoomId={uri.Segments[2]}");
            }
        }
    }
}