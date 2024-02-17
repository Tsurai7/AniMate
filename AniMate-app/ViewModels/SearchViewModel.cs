using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        public GenreCollection TitlesCollection { get; private set; }

        private Queue<Title> _searchResult = new();

        private int _loadedCount = 0;

        private readonly AnilibriaService _anilibriaService;

        private string _nameToFind;

        private bool _isLoading = false;

        private int _loadMoreResultsOffset = 6;

        public SearchViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;

            TitlesCollection = new("Search");
        }

        public async Task FindTitles(string name)
        {
            ClearSearchData();

            if (string.IsNullOrEmpty(name))
                return;

            _nameToFind = name;

            _searchResult = new(await _anilibriaService.GetTitlesByName(_nameToFind, 0, 6));

            if (_searchResult.Count.Equals(0))
                return;

            TitlesCollection.TargetTitleCount = _searchResult.Count > 6 ? 6 : _searchResult.Count;

            await LoadMoreResults();
        }

        public void ClearSearchData()
        {
            TitlesCollection.Clear();

            _loadedCount = 0;
        }

        [RelayCommand]
        public async Task LoadMoreResults()
        {
            if(_isLoading) 
                return;

            if(_searchResult.Count.Equals(0))
                return;

            _isLoading = true;

            int count = _searchResult.Count.Equals(TitlesCollection.TargetTitleCount) ? TitlesCollection.TargetTitleCount : _searchResult.Count;

            for(int i = _loadedCount; i < count; i++)
                TitlesCollection.AddTitle(_searchResult.Dequeue());

            _loadedCount = TitlesCollection.TargetTitleCount;

            TitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

            foreach (var title in await _anilibriaService.GetTitlesByName(_nameToFind, skip: _loadedCount, _loadedCount + _loadMoreResultsOffset))
                _searchResult.Enqueue(title);

            _isLoading = false;
        }
    }
}
