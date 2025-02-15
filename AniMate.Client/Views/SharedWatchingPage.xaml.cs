using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;

namespace AniMate_app.Views;

public partial class SharedWatchingPage : ContentPage
{
    private readonly SharedWatchingViewModel _viewModel;

    public SharedWatchingPage(SharedWatchingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;

        _viewModel._client.SyncState += OnSyncState;
        _viewModel._client.Paused += OnPaused;
        _viewModel._client.Resumed += OnResumed;
        _viewModel._client.Seeked += OnSeeked;
        _viewModel._client.VideoUrlUpdated += OnVideoUrlUpdated;
        _viewModel._client.MessageReceived += OnMessageReceived;
        _viewModel._client.RoomCreated += OnRoomCreated;

        MediaControl.SeekCompleted += OnSeekCompleted;
        MediaControl.StateChanged += OnMediaElementStateChanged;

        ChatMessagesListView.ItemsSource = _viewModel._chatMessages;
    }

    private void OnMediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        if (!_viewModel.HasConnection)
            return;

        switch (e.NewState)
        {
            case MediaElementState.Playing:
                Dispatcher?.Dispatch(async () => {
                    await _viewModel.Resume(_viewModel.RoomId, MediaControl.Position.TotalSeconds);
                });
                break;

            case MediaElementState.Paused:
                Dispatcher?.Dispatch(async () =>
                {
                    await _viewModel.Pause(_viewModel.RoomId, MediaControl.Position.TotalSeconds);
                });
                break;
        }
    }

    private void OnRoomCreated(string roomId)
    {
        _viewModel.RoomId = roomId;
    }

    private void OnSeekCompleted(object sender, EventArgs e)
    {
        Dispatcher?.Dispatch(async () =>
        {
            await _viewModel.Seek(MediaControl.Position.TotalSeconds);
        });
    }
        

    private void OnSyncState(string url, double timing, bool isPlaying)
    {
        Dispatcher.Dispatch(() =>
        {
            if (MediaControl.Source?.ToString() != url)
            {
                MediaControl.Source = url;
            }

            MediaControl.Pause();
            MediaControl.SeekTo(TimeSpan.FromSeconds(timing));
            MediaControl.Play();

            if (!isPlaying)
            {
                MediaControl.Pause();
            }
        });
    }


    private void OnPaused(string roomName, double timing)
    {
        Dispatcher.Dispatch(() =>
        {
            MediaControl.Pause();
        });
    }

    private void OnResumed(string roomName, double timing)
    {
        Dispatcher.Dispatch(() =>
        {
            MediaControl.Play();
        });
    }

    private void OnSeeked(string roomName, double newTime)
    {
        Dispatcher?.Dispatch(async () =>
        {
            await MediaControl.SeekTo(TimeSpan.FromSeconds(newTime));
        });
    }

    private async void OnEpisodeSelected(object sender, EventArgs e)
    {
        var selectedEpisodeText = EpisodePicker.SelectedItem?.ToString();

        if (selectedEpisodeText != null)
        {
            var episodeOrdinal = int.Parse(selectedEpisodeText.Split(':')[0].Replace("Серия", "").Trim());

            var selectedEpisode = _viewModel.Title.Player.Episodes.Values
                .FirstOrDefault(e => e.Ordinal == episodeOrdinal);

            if (selectedEpisode != null)
            {
                var episodeUrl = selectedEpisode.HlsUrls.Fhd;

                await _viewModel.ChangeVideoUrl(episodeUrl);
            }
        }
    }

    private void OnVideoUrlUpdated(string newUrl)
    {
        Dispatcher.Dispatch(() => { MediaControl.Source = newUrl; });
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await _viewModel.Disconnect();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.Connect();

        if (_viewModel.Title == null)
            return;

        foreach (var episode in _viewModel.Title.Player.Episodes.Values)
        {
            EpisodePicker.Items.Add($"Серия {episode.Ordinal}: {episode.Name}");
        }

        if (EpisodePicker.Items.Count > 0)
        {
            EpisodePicker.SelectedIndex = 0;
        }
    }

    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(MessageEntry.Text))
        {
            await _viewModel.SendMessage(MessageEntry.Text);
            MessageEntry.Text = string.Empty;
        }
    }

    private void OnMessageReceived(string message)
    {
        // TODO: Fix message duplication
        if (_viewModel._chatMessages.Count == 0 || _viewModel._chatMessages.Last() != message)
        {
            _viewModel._chatMessages.Add(message);
            ChatMessagesListView.ScrollTo(_viewModel._chatMessages[^1], ScrollToPosition.End, true);
        }
    }

    private async void OnCopyRoomCodeClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(RoomCodeLabel.Text))
        {
            await Clipboard.SetTextAsync(RoomCodeLabel.Text);
        }
    }

    private void ShareRoomLinkButtonClicked(object sender, EventArgs e)
    {
        _viewModel.ShareRoomLink();
    }
}
