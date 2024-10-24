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

        MediaControl.SeekCompleted += OnSeekCompleted;
        MediaControl.StateChanged += OnMediaElementStateChanged;
        
        ChatMessagesListView.ItemsSource = _viewModel._chatMessages;
    }

    private async void OnMediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        switch (e.NewState)
        {
            case MediaElementState.Playing:
                if (_viewModel._client != null)
                {
                    await _viewModel._client.Resume(_viewModel.RoomCode, MediaControl.Position.TotalSeconds);
                }
                break;
            case MediaElementState.Paused:
                if (_viewModel._client != null)
                {
                    await _viewModel._client.Pause(_viewModel.RoomCode, MediaControl.Position.TotalSeconds);
                }
                break;
        }
    }

    private async void OnSeekCompleted(object sender, EventArgs e) =>
        await _viewModel._client.Seek(_viewModel.RoomCode, MediaControl.Position.TotalSeconds);

    private void OnSyncState(string url, double timing, bool isPlaying)
    {
        Dispatcher.Dispatch(() =>
        {
            MediaControl.Source = url;
            MediaControl.SeekTo(TimeSpan.FromSeconds(timing));
            if (isPlaying)
            {
                MediaControl.Play();
            }
            else
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
        => MediaControl.SeekTo(TimeSpan.FromSeconds(newTime));

    private async void OnEpisodeSelected(object sender, EventArgs e)
    {
        var selectedEpisode = EpisodePicker.SelectedItem?.ToString();
        if (selectedEpisode != null)
        {
            await _viewModel._client.UpdateVideoUrl(_viewModel.RoomCode, selectedEpisode);
        }
    }

    private void OnVideoUrlUpdated(string newUrl) 
        => MediaControl.Source = newUrl;

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await _viewModel._client.DisconnectAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel._client.ConnectAsync();
        await _viewModel._client.CreateRoom(_viewModel.RoomCode, "test", _viewModel.MediaUrl);
    }

    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(MessageEntry.Text))
        {
            await _viewModel._client.SendMessage(_viewModel.RoomCode, MessageEntry.Text); 
            MessageEntry.Text = string.Empty;
        }
    }

    private void OnMessageReceived(string message)
    {
        _viewModel._chatMessages.Add(message);
        ChatMessagesListView.ScrollTo(_viewModel._chatMessages[^1], ScrollToPosition.End, true);
    }

    private async void OnCopyRoomCodeClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(RoomCodeLabel.Text))
        {
            await Clipboard.SetTextAsync(RoomCodeLabel.Text);
            await DisplayAlert("Скопировано", "Код комнаты скопирован в буфер обмена.", "OK");
        }
    }
}
