using AniMate_app.DTOs.Account;
using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(ProfileDto), "Profile")]
    public partial class ProfileViewModel : ViewModelBase
    {
        private readonly IAccountClient _accountClient;
        private readonly IAnimeClient _animeClient;

        [ObservableProperty] 
        public ProfileDto _profileInfo = new ("Nikita Desuyo",
            "https://pm1.aminoapps.com/7796/1f2d2bbecb5816f2ed8d540e6f9da0ef900c2fdbr1-736-736v2_uhq.jpg",
            "nikita@gmail.com", ["nanatsu-no-taizai-kamigami-no-gekirin", "jujutsu-kaisen"], ["jujutsu-kaisen"]);
        
        [ObservableProperty]
        private GenreCollection _likedTitlesCollection = new("Likes");
        
        [ObservableProperty]
        private GenreCollection _watchedTitlesCollection = new("Watched");

        [ObservableProperty]
        private bool _isLoading = false;

        private int LoadedTitles => LikedTitlesCollection.TitleCount;
        
        private readonly int _loadMoreResultsOffset = 6;
        
        public ProfileViewModel(IAccountClient accountClient, IAnimeClient animeClient)
        {
            _accountClient = accountClient;
            _animeClient = animeClient;
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

            ProfileInfo = null;

            await LoadContent();

            IsRefreshing = false;

            IsBusy = false;
        }
        public override async Task LoadContent()
        {
            var accessToken = Preferences.Default.Get("AccessToken", string.Empty);
            LikedTitlesCollection.Clear();
            WatchedTitlesCollection.Clear();
            //ProfileInfo = await _accountClient.GetProfileInfo(accessToken);
            
            var likedTitles = await _animeClient.GetTitlesByCode(ProfileInfo.LikedTitles);
            if (likedTitles != null)
            {
                LikedTitlesCollection.AddTitleList(likedTitles);
                LikedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;
            }

            var watchedTitles = await _animeClient.GetTitlesByCode(ProfileInfo.WatchedTitles);
            if (watchedTitles != null)
            {
                WatchedTitlesCollection.AddTitleList(watchedTitles);
                WatchedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;
            }
            
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

            if(ProfileInfo.LikedTitles.Count <= LikedTitlesCollection.TargetTitleCount)
            {
                return;
            }

            if (ProfileInfo.WatchedTitles.Count <= WatchedTitlesCollection.TargetTitleCount)
            {
                return;
            }

            IsLoading = true;

            var loadedTitles = await _animeClient.GetTitlesByCode(ProfileInfo.LikedTitles,
                LikedTitlesCollection.TitleCount, LikedTitlesCollection.TargetTitleCount);
            if (loadedTitles.Count > 0)
                LikedTitlesCollection.AddTitleList(loadedTitles);


            WatchedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

            var loadedWatchedTitles = await _animeClient.GetTitlesByCode(ProfileInfo.WatchedTitles,
                WatchedTitlesCollection.TitleCount, WatchedTitlesCollection.TargetTitleCount);
            if (loadedWatchedTitles.Count > 0)
                WatchedTitlesCollection.AddTitleList(loadedWatchedTitles);
            IsLoading = false;
        }
    }
}