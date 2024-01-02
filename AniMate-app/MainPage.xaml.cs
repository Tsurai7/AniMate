namespace AniMate_app
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            mediaElement.MediaOpened += MediaElement_MediaOpened;
            mediaElement.MediaFailed += MediaElement_MediaFailed;
            mediaElement.MediaEnded += MediaElement_MediaEnded;
        }
        private void MediaElement_MediaOpened(object sender, EventArgs e)
        {
            // Handle media opened event
            // This is triggered when the media is successfully opened
        }

        private void MediaElement_MediaFailed(object sender, EventArgs e)
        {
            // Handle media failed event
            // This is triggered when there is an issue opening the media
        }

        private void MediaElement_MediaEnded(object sender, EventArgs e)
        {
            // Handle media ended event
            // This is triggered when the media playback reaches the end
        }
    }
}