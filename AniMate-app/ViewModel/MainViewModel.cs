using AniMate_app.Anilibria;
using AniMate_app.Model;
using AniMate_app.Utils;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModel
{
    class MainViewModel : BindableObject
    {
        public ObservableCollection<GenreCollection> TitlesByGenre { get; private set; } = new();

        public List<string> Genres { get; private set; }

        public int GenresLoaded { get; private set; } = 0;

        public int RemainingItems { get; private set; } = 1;

        public bool AllGenresLoaded => GenresLoaded.Equals(Genres.Count);

        public bool IsTitlesLoaded { get; private set; } = true;

        public bool IsGenresLoaded { get; private set; } = true;

        private CommandCollection<int> _loadGenreCommandsQueue = new();

        private CommandCollection<GenreCollection> _loadMoreTitlesCommandsQueue = new();

        private Utils.Command<int> LoadNewGenreTitles => new(5, LoadTitlesByGenreAction, CanLoadMoreGenres);

        private int _loadMoreTitlesCount = 5;

        public async Task LoadContent()
        {
            Genres = await AnilibriaAPI.GetGenres();

            TitlesByGenre.Clear();

            _loadGenreCommandsQueue.Clear();

            GenresLoaded = 0;

            LoadGenres();

            _loadGenreCommandsQueue.Add(LoadNewGenreTitles);
        }


        public void LoadMoreTitlesForGenre(GenreCollection genreCollection)
        {
            if (genreCollection.TargetTitleCount.Equals(genreCollection.TitleCount))
                _loadMoreTitlesCommandsQueue.Add(new(genreCollection, LoadMoreTitlesForGenreAction, CanLoadMoreTitlesForeGenre));

            else if (_loadMoreTitlesCommandsQueue.CommandCount > 0)
                _loadMoreTitlesCommandsQueue.Clear();
        }

        private bool CanLoadMoreTitlesForeGenre(GenreCollection genreCollection)
        {
            return genreCollection.TargetTitleCount.Equals(genreCollection.TitleCount) && IsTitlesLoaded;
        }

        private async void LoadMoreTitlesForGenreAction(GenreCollection genreCollection)
        {
            if (!IsTitlesLoaded)
                return;

            IsTitlesLoaded = false;

            if (genreCollection.TargetTitleCount > genreCollection.TitleCount)
                return;

            genreCollection.TargetTitleCount += _loadMoreTitlesCount;

            genreCollection.AddTitleList(await AnilibriaAPI.GetTilesByGenre(genreCollection.GenreName, genreCollection.TitleCount, _loadMoreTitlesCount));

            IsTitlesLoaded = true;
        }

        private bool CanLoadMoreGenres(int count)
        {
            return IsGenresLoaded && !AllGenresLoaded;
        }

        private void LoadGenres()
        {
            foreach (var genre in Genres)
                TitlesByGenre.Add(new(genre));
        }

        public void LoadMoreGenres()
        {
            if (!AllGenresLoaded)
                _loadGenreCommandsQueue.Add(LoadNewGenreTitles);
            else if (_loadGenreCommandsQueue.CommandCount > 0)
                _loadGenreCommandsQueue.Clear();
        }

        private async void LoadTitlesByGenreAction(int count)
        {
            if(!IsGenresLoaded) return;

            IsGenresLoaded = false;

            int newCount = GenresLoaded + count < Genres.Count ? GenresLoaded + count : Genres.Count;

            for (int i = GenresLoaded; i < newCount; i++)
            {
                var genreTitles = TitlesByGenre[i];

                genreTitles.AddTitleList(await AnilibriaAPI.GetTilesByGenre(Genres[i], 0, 5));

                genreTitles.TargetTitleCount = count;
            }

            GenresLoaded = newCount;

            IsGenresLoaded = true;

            RemainingItems = Genres.Count - GenresLoaded;

            OnPropertyChanged(nameof(RemainingItems));
        }
    }
}
