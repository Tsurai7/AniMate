using AniMate_app.Utils;
using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;

namespace AniMate_app.Views;

public partial class SharedWatchingPage : ContentPage
{
    private readonly SharedWatchingViewModel _viewModel;

    private int _eventSkipCount = 0;

    public SharedWatchingPage(SharedWatchingViewModel viewModel)
    {
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        BindingContext = _viewModel = viewModel;

        _viewModel._client.SyncState += OnSyncState;
        _viewModel._client.SyncTitle += OnSyncTitle;
        _viewModel._client.Paused += OnPaused;
        _viewModel._client.Resumed += OnResumed;
        _viewModel._client.Seeked += OnSeeked;
        _viewModel._client.VideoUrlUpdated += OnVideoUrlUpdated;
        _viewModel._client.MessageReceived += OnMessageReceived;
        _viewModel._client.RoomCreated += OnRoomCreated;
        _viewModel._client.Error += OnError;
        _viewModel._client.DisconnectRequested += OnDisconnectRequested;

        MediaControl.StateChanged += OnMediaElementStateChanged;

        ChatMessagesListView.ItemsSource = _viewModel.ChatMessages;
    }

    private void OnMediaElementStateChanged(object? sender, MediaStateChangedEventArgs e)
    {
        if (_eventSkipCount > 0)
        {
            _eventSkipCount--;
            return;
        }

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

    private void OnSyncTitle(string titleCode)
    {
        _viewModel.LoadTitle(titleCode);
    }

    private async void OnDisconnectRequested()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void OnSyncState(string url, double timing, bool isPlaying)
    {
        Dispatcher?.Dispatch(() =>
        {
            if (MediaControl.Source?.ToString() != url)
            {
                MediaControl.Source = url;
            }

            _eventSkipCount += 4;

            MediaControl.SeekTo(TimeSpan.FromSeconds(timing));
        });

        Dispatcher?.Dispatch(() =>
        {
            if (!isPlaying)
                MediaControl.Pause();
            else MediaControl.Play();
        });
    }


    private void OnPaused(string roomName, double timing)
    {
        Dispatcher?.Dispatch(() =>
        {
            var newTiming = TimeSpan.FromSeconds(timing);

            _eventSkipCount += 4;
            MediaControl.SeekTo(newTiming);

            MediaControl.Pause();
        });
    }

    private void OnResumed(string roomName, double timing)
    {
        Dispatcher?.Dispatch(() =>
        {
            var newTiming = TimeSpan.FromSeconds(timing);

            _eventSkipCount += 4;
            MediaControl.SeekTo(newTiming);

            MediaControl.Play(); 
        });
    }

    private void OnSeeked(string roomName, double newTime)
    {
        Dispatcher?.Dispatch(async () =>
        {
            _eventSkipCount += 4;
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
        Dispatcher?.Dispatch(() => { MediaControl.Source = newUrl; });
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.RestrictFullScreen();
        Dispatcher?.Dispatch(() => { MediaControl.Stop(); MediaControl.Handler.DisconnectHandler(); });
        await _viewModel.Disconnect();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.AllowFullScreen();

        await _viewModel.Connect();

        if (_viewModel.Title == null)
            return;

        _viewModel.Title.Player.Episodes?.Values.Map((episode) => EpisodePicker.Items.Add($"Серия {episode.Ordinal}: {episode.Name}"));

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
        if (_viewModel.ChatMessages.Count == 0 || _viewModel.ChatMessages.Last() != message)
        {
            _viewModel.ChatMessages.Add(message);
            ChatMessagesListView.ScrollTo(_viewModel.ChatMessages[^1], ScrollToPosition.End, true);
        }
    }

    private void ShareRoomLinkButtonClicked(object sender, EventArgs e)
    {
        _viewModel.ShareRoomCode();
    }

    private void OnError(string message)
    {
        //Shell.Current.GoToAsync(nameof(MainPage));
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
