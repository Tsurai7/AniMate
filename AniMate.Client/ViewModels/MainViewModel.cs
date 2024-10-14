using AniMate_app.Interfaces;
using AniMate_app.Models;
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

        public readonly IAnimeClient AnimeClient;

        public MainViewModel(IAnimeClient animeClient)
        {
            AnimeClient = animeClient;

            _loadMoreContentOffset = 4;
        }

        public override async Task LoadContent()
        {
            IsBusy = true;

            GenreList = new(_loadMoreContentOffset);

            Genres = await AnimeClient.GetAllGenres();

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

            var newGenres = await LoadGenres(count);

            GenreList.AddRange(newGenres);

            IsLoading = false;
        }

        private async Task<List<GenreCollection>> LoadGenres(int count)
        {
            var newCount = GenresLoaded + count < Genres.Count ? GenresLoaded + count : Genres.Count;

            List<GenreCollection> list = [];

            for (var i = GenresLoaded; i < newCount; i++)
            {
                GenreCollection genreCollection = new(Genres[i]);

                genreCollection.AddTitleList(await AnimeClient.GetTitlesByGenre(Genres[i], 0, _loadTitlesCount));

                genreCollection.TargetTitleCount = _loadTitlesCount;

                list.Add(genreCollection);

                GenresLoaded++;
            }

            return list;
        }
    }
}
