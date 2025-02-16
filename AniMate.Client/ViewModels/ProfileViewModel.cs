using AniMate_app.Clients;
using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profile = AniMate_app.Models.Profile;

namespace AniMate_app.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private readonly AccountClient _accountClient;
    private readonly IAnimeClient _animeClient;
    
    private Profile _profile;
    
    public Profile Profile
    {
        get => _profile;
    }
    
    [ObservableProperty]
    private GenreCollection _likedTitlesCollection = new("Likes");
    
    [ObservableProperty]
    private GenreCollection _watchedTitlesCollection = new("Watched");

    [ObservableProperty]
    private bool _isLoading = false;

    private int LoadedTitles => LikedTitlesCollection.TitleCount;
    
    private readonly int _loadMoreResultsOffset = 6;
    
    public ProfileViewModel(
        AccountClient accountClient,
        IAnimeClient animeClient,
        Profile profile)
    {
        _accountClient = accountClient;
        _animeClient = animeClient;
        _profile = profile;
    }

    [RelayCommand]
    public async Task Refresh()
    {
        if (IsBusy)
        {
            IsRefreshing = false;

            return;
        }

        IsBusy = true;

        await LoadContent();

        IsRefreshing = false;

        IsBusy = false;
    }
    
    public override async Task LoadContent()
    {
    }

    [RelayCommand]
    public override async Task LoadMoreContent()
    {
        if (IsLoading || IsRefreshing)
            return;

        if (LikedTitlesCollection.TargetTitleCount > LikedTitlesCollection.TitleCount)
            return;

        if (WatchedTitlesCollection.TargetTitleCount > WatchedTitlesCollection.TitleCount)
            return;

        LikedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

        WatchedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

        if(Profile.LikedTitles.Count <= LikedTitlesCollection.TargetTitleCount)
        {
            return;
        }

        if (Profile.WatchedTitles.Count <= WatchedTitlesCollection.TargetTitleCount)
        {
            return;
        }

        IsLoading = true;

        var loadedTitles = await _animeClient.GetTitlesByCode(Profile.LikedTitles,
            LikedTitlesCollection.TitleCount, LikedTitlesCollection.TargetTitleCount);
        if (loadedTitles.Count > 0)
            LikedTitlesCollection.AddTitleList(loadedTitles);


        WatchedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

        var loadedWatchedTitles = await _animeClient.GetTitlesByCode(Profile.WatchedTitles,
            WatchedTitlesCollection.TitleCount, WatchedTitlesCollection.TargetTitleCount);
        if (loadedWatchedTitles.Count > 0)
            WatchedTitlesCollection.AddTitleList(loadedWatchedTitles);
        IsLoading = false;
    }
}
