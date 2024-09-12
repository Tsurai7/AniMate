using System.Collections.Generic;
using System.Threading.Tasks;
using AniMate_app.Model;
using AniMate_app.Services;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private List<GenreCollection> _genreList;

        public List<string> Genres { get; private set; }

        public int GenresLoaded { get; private set; } = 0;

        public bool AllGenresLoaded => GenresLoaded.Equals(Genres.Count);

        [ObservableProperty]
        private bool _isLoadingTitles = false;

        private readonly int _loadTitlesCount = 4;

        public readonly AnilibriaService _anilibriaService;

        public MainViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;

            _loadMoreContentOffset = 4;
        }

        public override async Task LoadContent()
        {
            IsBusy = true;

            GenreList = new(_loadMoreContentOffset);

            Genres = await _anilibriaService.GetAllGenres();

            await LoadMoreGenres(_loadMoreContentOffset);

            IsBusy = false;
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

            GenresLoaded = 0;

            await LoadContent();

            IsRefreshing = false;

            IsBusy = false;
        }

        [RelayCommand]
        public override async Task LoadMoreContent()
        {
            if (!IsBusy && !IsLoading && !AllGenresLoaded)
                await LoadMoreGenres(_loadMoreContentOffset);
        }

        private async Task LoadMoreGenres(int count)
        {
            IsLoading = true;

            List<GenreCollection> newGenres = await LoadGenres(count);

            GenreList.AddRange(newGenres);

            IsLoading = false;
        }

        private async Task<List<GenreCollection>> LoadGenres(int count)
        {
            int newCount = GenresLoaded + count < Genres.Count ? GenresLoaded + count : Genres.Count;

            List<GenreCollection> list = new();

            for (int i = GenresLoaded; i < newCount; i++)
            {
                GenreCollection genreCollection = new(Genres[i]);

                genreCollection.AddTitleList(await _anilibriaService.GetTitlesByGenre(Genres[i], 0, _loadTitlesCount));

                genreCollection.TargetTitleCount = _loadTitlesCount;

                list.Add(genreCollection);

                GenresLoaded++;
            }

            return list;
        }
    }
}
