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

        public string Test {  get; private set; }

        public int GenresLoaded { get; private set; } = 0;

        public int ReaminingItemsThereshold { get; private set; } = 1;

        public bool AllGenresLoaded => GenresLoaded.Equals(Genres.Count);

        public bool IsLoaded { get; private set; } = true;

        private CommandCollection<int> _loadCommandsQueue = new();

        private Utils.Command<int> LoadMoreTitlesCommand => new(5, LoadTitlesByGenre, CanLoadMore);

        public async Task LoadContent()
        {
            Genres = await AnilibriaAPI.GetGenres();

            TitlesByGenre.Clear();

            //_loadCommandsQueue.Clear();

            GenresLoaded = 0;

            LoadGenres();

            _loadCommandsQueue.Add(LoadMoreTitlesCommand);
        }

        public void LoadMoreGenres()
        {
            if(!AllGenresLoaded)
                _loadCommandsQueue.Add(LoadMoreTitlesCommand);
            else
                _loadCommandsQueue.Clear();
        }

        private bool CanLoadMore(int count)
        {
            return IsLoaded && !AllGenresLoaded;
        }

        private void LoadGenres()
        {
            foreach (var genre in Genres)
                TitlesByGenre.Add(new(genre));
        }

        public async void LoadTitlesByGenre(int count)
        {
            if(!IsLoaded) return;

            IsLoaded = false;

            count = GenresLoaded + count < Genres.Count ? GenresLoaded + count : Genres.Count;

            for (int i = GenresLoaded; i < count; i++)
                TitlesByGenre[i].AddTitleList(await AnilibriaAPI.GetTilesByGenre(Genres[i]));

            GenresLoaded = count;

            IsLoaded = true;

            ReaminingItemsThereshold = Genres.Count - GenresLoaded;

            OnPropertyChanged(nameof(ReaminingItemsThereshold));
        }
    }
}
