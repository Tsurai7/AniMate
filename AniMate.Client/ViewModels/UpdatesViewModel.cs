using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class UpdatesViewModel : ViewModelBase
    {
        private readonly IAnimeClient _animeClient;
        
        [ObservableProperty]
        private GenreCollection _titles = new("updates");

        [ObservableProperty]
        private string _labelText;

        [ObservableProperty]
        private GenreCollection _resumeWatchList = new("resume");

        public UpdatesViewModel(IAnimeClient animeClient)
        {
            _animeClient = animeClient;

            _loadMoreContentOffset = 4;
        }

        public override async Task LoadContent()
        {
            await LoadMoreContent();
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            Titles.AddTitleList(await _animeClient.GetUpdates(Titles.TitleCount, _loadMoreContentOffset));
        
            IsLoading = false;
        }

        [RelayCommand]
        public async Task Refresh()
        {
            IsBusy = IsRefreshing = true;

            Titles.Clear();

            ResumeWatchList.Clear();

            await LoadMoreContent();

            //await LoadSavedData();

            IsBusy = IsRefreshing = false;
        }

        //public async Task LoadSavedData()
        //{
        //    if (Preferences.Default.ContainsKey("visited"))
        //    {
        //        var lastVisited = Preferences.Default.Get<string>("visited", default).Split(';');

        //        foreach (string code in lastVisited)
        //            ResumeWatchList.AddTitle(await _anilibriaService.GetTitleByCode(code));
        //    }
        //}
    }
}
