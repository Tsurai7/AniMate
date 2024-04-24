using AniMate_app.Model;
using AniMate_app.Services.AccountService;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class UpdatesViewModel : ViewModelBase
    {
        private AnilibriaService _anilibriaService;

        [ObservableProperty]
        private GenreCollection _titles = new("updates");

        [ObservableProperty]
        private string _labelText;

        [ObservableProperty]
        private GenreCollection _resumeWatchList = new("resume");

        public UpdatesViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;
        }

        public override async Task LoadContent()
        {
            var titles = await _anilibriaService.GetUpdates(0, _loadMoreContentOffset);

            Titles.AddTitleList(titles);
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            Titles.AddTitleList(await _anilibriaService.GetUpdates(Titles.TitleCount, _loadMoreContentOffset));
        }

        [RelayCommand]
        public async Task Refresh()
        {
            IsBusy = IsRefreshing = true;

            Titles.Clear();

            ResumeWatchList.Clear();

            await LoadContent();

            await LoadSavedData();

            IsBusy = IsRefreshing = false;
        }

        public async Task LoadSavedData()
        {
            if (Preferences.Default.ContainsKey("visited"))
            {
                var lastVisited = Preferences.Default.Get<string>("visited", default).Split(';');

                foreach (string code in lastVisited)
                    ResumeWatchList.AddTitle(await _anilibriaService.GetTitleByCode(code));
            }
        }
    }
}
