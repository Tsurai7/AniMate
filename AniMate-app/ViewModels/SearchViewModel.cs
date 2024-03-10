using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class SearchViewModel : ViewModelBase
    {
        public GenreCollection TitlesCollection { get; private set; }

        private readonly AnilibriaService _anilibriaService;

        private string _nameToFind;

        public SearchViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;

            TitlesCollection = new("Search");

            _loadMoreContentOffset = 12;
        }

        public async Task FindTitles(string name)
        {
            ClearSearchData();

            if (string.IsNullOrEmpty(name))
                return;

            IsBusy = true;

            _nameToFind = name;

            var result = await _anilibriaService.GetTitlesByName(_nameToFind, 0, _loadMoreContentOffset);

            if (result.Count.Equals(0))
            {
                IsBusy = false;

                return;
            }
                

            TitlesCollection.TargetTitleCount = result.Count > _loadMoreContentOffset ? _loadMoreContentOffset : result.Count;

            IsBusy = false;

            TitlesCollection.AddTitleList(result);
        }

        public void ClearSearchData()
        {
            TitlesCollection.Clear();
        }

        public override Task LoadContent()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            if(IsLoading)
                return;

            if(TitlesCollection.TargetTitleCount > TitlesCollection.TitleCount)
                return;

            IsLoading = true;

            TitlesCollection.TargetTitleCount += _loadMoreContentOffset;

            TitlesCollection.AddTitleList(await _anilibriaService.GetTitlesByName(_nameToFind, skip: TitlesCollection.TitleCount, TitlesCollection.TitleCount + _loadMoreContentOffset));

            IsLoading = false;
        }
    }
}
