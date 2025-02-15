using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;

namespace AniMate_app.Views;

public partial class SharedWatchingPage : ContentPage
{
    private readonly SharedWatchingViewModel _viewModel;

    private bool _innerPauseRequest = false;

    private bool _innerResumeRequest = false;

    private bool _innerSeekRequest = false;

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
        
        ChatMessagesListView.ItemsSource = _viewModel._chatMessages;
    }

    private void OnMediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        if (_viewModel.HasConnection)
        {
            Dispatcher.Dispatch(() =>
            {
                switch (e.NewState)
                {
                    case MediaElementState.Playing:
                        if(!_innerResumeRequest)
                            _viewModel.Resume(_viewModel.RoomId, MediaControl.Position.TotalSeconds);

                        _innerResumeRequest = false;

                        break;
                    case MediaElementState.Paused:
                        if(!_innerPauseRequest)
                            _viewModel.Pause(_viewModel.RoomId, MediaControl.Position.TotalSeconds);

                            _innerPauseRequest = false;
                        
                        break;
                }
            });
        }
    }
    
    private void OnRoomCreated(string roomId)
    {
        _viewModel.RoomId = roomId;
    }
    
    private void OnSeekCompleted(object sender, EventArgs e)
    {
        if (!_innerSeekRequest)
            _viewModel.Seek(MediaControl.Position.TotalSeconds);

        _innerSeekRequest = false;
    }

    private void OnSyncState(string url, double timing, bool isPlaying)
    {
        Dispatcher.Dispatch(async () =>
        {
            if (MediaControl.Source?.ToString() != url)
            {
                OnVideoUrlUpdated(url);
            }

            _innerSeekRequest = true;

            await MediaControl.SeekTo(TimeSpan.FromSeconds(timing));
        });

        Dispatcher?.Dispatch(() =>
        {
            if (isPlaying)
            {
                _innerResumeRequest = true;

                MediaControl.Play();
            }
            else
            {
                _innerPauseRequest = true;

                MediaControl.Pause();
            }
        });
    }

    private void OnPaused(string roomName, double timing)
    {
        Dispatcher?.Dispatch(() =>
        {
            _innerPauseRequest = true;

            MediaControl.Pause();
        });

        Dispatcher?.Dispatch(() =>
        {
            var newTiming = TimeSpan.FromSeconds(timing);

            if (!MediaControl.Position.Equals(newTiming))
            {
                _innerSeekRequest = true;

                MediaControl.SeekTo(newTiming);
            }
        }); 
    }

    private void OnResumed(string roomName, double timing)
    {
        Dispatcher?.Dispatch(() =>
        {
            _innerResumeRequest = true;

            MediaControl.Play();
        });

        Dispatcher?.Dispatch(() =>
        {
            var newTiming = TimeSpan.FromSeconds(timing);

            if (!MediaControl.Position.Equals(newTiming))
            {
                _innerSeekRequest = true;

                MediaControl.SeekTo(newTiming);
            }
        }); 
    }

    private void OnSeeked(string roomName, double newTime)
    {
        Dispatcher?.Dispatch(async () =>
        {
            _innerSeekRequest = true;

            await MediaControl.SeekTo(TimeSpan.FromSeconds(newTime));
        });
    }

    private void OnEpisodeSelected(object sender, EventArgs e)
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

                _viewModel.ChangeVideoUrl(episodeUrl);
            }
        }
    }

    private void OnVideoUrlUpdated(string newUrl)
    {
        Dispatcher.Dispatch(() =>
        {
            _innerPauseRequest = true;

            MediaControl.Source = newUrl;
        });
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        Dispatcher?.Dispatch(() =>
        {

            MediaControl.Stop();

            MediaControl.Handler?.DisconnectHandler();

            MediaControl.Source = null;
        });

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

    private void OnSendMessageClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(MessageEntry.Text))
        {
            _viewModel.SendMessage(MessageEntry.Text);

            MessageEntry.Text = string.Empty;
        }
    }

    private void OnMessageReceived(string message)
    {
        // TODO: Fix message duplication
        if (_viewModel._chatMessages.Count == 0 || _viewModel._chatMessages.Last() != message)
        {
            _viewModel.AddMessage(message);

            ChatMessagesListView.ScrollTo(_viewModel._chatMessages[^1], ScrollToPosition.End, true);
        }
    }

    private void ShareRoomLinkButtonClicked(object sender, EventArgs e)
    {
        _viewModel.ShareRoomLink();
    }
}
