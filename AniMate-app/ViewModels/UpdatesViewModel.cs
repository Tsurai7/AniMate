using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class UpdatesViewModel : ViewModelBase
    {
        private AnilibriaService _anilibriaService;

        [ObservableProperty]
        private List<Title> _titles = new();

        [ObservableProperty]
        private List<Title> _resumeWatchList = new();

        public UpdatesViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;
        }

        public override async Task LoadContent()
        {
            var titles = await _anilibriaService.GetUpdates(0, _loadMoreContentOffset);

            Titles.AddRange(titles);

            ResumeWatchList.AddRange(titles);
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            Titles.AddRange(await _anilibriaService.GetUpdates(Titles.Count, _loadMoreContentOffset));
        }
    }
}
