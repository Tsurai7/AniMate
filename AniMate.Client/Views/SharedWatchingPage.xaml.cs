using AniMate_app.Clients;
using AniMate_app.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;

namespace AniMate_app.Views;

public partial class SharedWatchingPage : ContentPage
{
    private readonly SharedWatchingClient _sharedWatchingService;
    
    private readonly SharedWatchingViewModel _viewModel;
    
    public SharedWatchingPage(SharedWatchingViewModel viewModel, SharedWatchingClient sharedWatchingService)
    {
        InitializeComponent();
        
        BindingContext = _viewModel = viewModel;
        _sharedWatchingService = sharedWatchingService;
        
        _sharedWatchingService.SyncState += OnSyncState;
        _sharedWatchingService.Paused += OnPaused;
        _sharedWatchingService.Resumed += OnResumed;
        _sharedWatchingService.Seeked += OnSeeked;
        _sharedWatchingService.VideoUrlUpdated += OnVideoUrlUpdated;
        _sharedWatchingService.MessageReceived += OnMessageReceived;
        
        MediaControl.SeekCompleted += OnSeekCompleted;
        MediaControl.StateChanged += OnMediaElementStateChanged;
        
        Task.Run(async () => await _sharedWatchingService.ConnectAsync());
        Task.Run(async () => await _sharedWatchingService.JoinRoom("123"));
        
        _viewModel._chatMessages = [];
        ChatMessagesListView.ItemsSource = _viewModel._chatMessages;
    }

    private async void OnMediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        switch (e.NewState)
        {
            case MediaElementState.Playing:
                await _sharedWatchingService.Resume("123", MediaControl.Position.TotalSeconds);
                break;
            case MediaElementState.Paused:
                await _sharedWatchingService.Pause("123", MediaControl.Position.TotalSeconds);
                break;
        }
    }

    private async void OnSeekCompleted(object sender, EventArgs e) =>
        await _sharedWatchingService.Seek("123", MediaControl.Position.TotalSeconds);

    private void OnSyncState(string url, double timing, bool isPlaying)
    {
        Device.BeginInvokeOnMainThread(() =>
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
        => MediaControl.Pause();
    
    private void OnResumed(string roomName, double timing) 
        => MediaControl.Play();

    private void OnSeeked(string roomName, double newTime) 
        => MediaControl.SeekTo(TimeSpan.FromSeconds(newTime));


    private async void OnEpisodeSelected(object sender, EventArgs e)
    {
        var selectedEpisode = EpisodePicker.SelectedItem?.ToString();
        if (selectedEpisode != null)
        {
            await _sharedWatchingService.UpdateVideoUrl("123", selectedEpisode);
        }
    }

    private void OnVideoUrlUpdated(string newUrl) 
        => MediaControl.Source = newUrl;

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await _sharedWatchingService.DisconnectAsync();
    }
    
    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(MessageEntry.Text))
        {
            await _sharedWatchingService.SendMessage("123", MessageEntry.Text); 
            
            MessageEntry.Text = string.Empty;
        }
    }
    
    private void OnMessageReceived(string message)
    {
        _viewModel._chatMessages.Add(message);
        ChatMessagesListView.ScrollTo(_viewModel._chatMessages[^1], ScrollToPosition.End, true);
    }
}