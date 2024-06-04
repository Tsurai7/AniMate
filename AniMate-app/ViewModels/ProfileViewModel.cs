using AniMate_app.Model;
using AniMate_app.Services.AccountService;
using AniMate_app.Services.AccountService.Dtos;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        private GenreCollection _likedTitlesCollection;
        
        [ObservableProperty]
        private GenreCollection _watchedTitlesCollection;

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

            ProfileInfo = await _accountService.GetProfileInfo(accessToken);

            LikedTitlesCollection = new GenreCollection("Liked Titles");

            LikedTitlesCollection.AddTitleList(await _anilibriaService.GetTitlesByCode(ProfileInfo.LikedTitles));

            LikedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;

            WatchedTitlesCollection.AddTitleList(await _anilibriaService.GetTitlesByCode(ProfileInfo.WatchedTitles));

            WatchedTitlesCollection.TargetTitleCount = _loadMoreResultsOffset;
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            if (IsLoading)
                return;

            if (LikedTitlesCollection.TargetTitleCount > LikedTitlesCollection.TitleCount)
                return;

            IsLoading = true;

            LikedTitlesCollection.TargetTitleCount += _loadMoreResultsOffset;
            if(ProfileInfo.LikedTitles.Count <= LikedTitlesCollection.TargetTitleCount)
            {
                return;
            }
            List<Title> loadedTitles = await _anilibriaService.GetTitlesByCode(ProfileInfo.LikedTitles,
                LikedTitlesCollection.TitleCount, LikedTitlesCollection.TargetTitleCount);
            if (loadedTitles.Count > 0)
                LikedTitlesCollection.AddTitleList(loadedTitles);
            IsLoading = false;
        }
    }
}