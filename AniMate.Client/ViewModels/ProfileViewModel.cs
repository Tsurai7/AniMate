using System.Collections.Generic;
using System.Threading.Tasks;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Anime;
using AniMate_app.Model;
using AniMate_app.Services;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(ProfileInfo), "Profile")]
    public partial class ProfileViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;

        public readonly AnilibriaService _anilibriaService;

        [ObservableProperty]
        private ProfileDto _profileInfo;

        public ProfileViewModel(AccountService accountService, AnilibriaService anilibriaService)
        {
            _accountService = accountService;

            _anilibriaService = anilibriaService;
        }

        [ObservableProperty]
        private GenreCollection _likedTitlesCollection = new("Likes");
        
        [ObservableProperty]
        private GenreCollection _watchedTitlesCollection = new("Watched");

        [ObservableProperty]
        private bool _isLoading = false;

        private int LoadedTitles => LikedTitlesCollection.TitleCount;

        private readonly int _loadMoreResultsOffset = 6;


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
            string accessToken = Preferences.Default.Get("AccessToken", string.Empty);
            LikedTitlesCollection.Clear();
            WatchedTitlesCollection.Clear();
            ProfileInfo = await _accountService.GetProfileInfo(accessToken);
            if (ProfileInfo != null)
            {
                var likedTitles = await _anilibriaService.GetTitlesByCode(ProfileInfo.LikedTitles);
                if (likedTitles != null)
                {
                    LikedTitlesCollection.AddTitleList(likedTitles);
                    LikedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;
                }

                var watchedTitles = await _anilibriaService.GetTitlesByCode(ProfileInfo.WatchedTitles);
                if (watchedTitles != null)
                {
                    WatchedTitlesCollection.AddTitleList(watchedTitles);
                    WatchedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;
                }
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

            List<TitleDto> loadedTitles = await _anilibriaService.GetTitlesByCode(ProfileInfo.LikedTitles,
                LikedTitlesCollection.TitleCount, LikedTitlesCollection.TargetTitleCount);
            if (loadedTitles.Count > 0)
                LikedTitlesCollection.AddTitleList(loadedTitles);


            WatchedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

            List<TitleDto> loadedWatchedTitles = await _anilibriaService.GetTitlesByCode(ProfileInfo.WatchedTitles,
                WatchedTitlesCollection.TitleCount, WatchedTitlesCollection.TargetTitleCount);
            if (loadedWatchedTitles.Count > 0)
                WatchedTitlesCollection.AddTitleList(loadedWatchedTitles);
            IsLoading = false;
        }
    }
}