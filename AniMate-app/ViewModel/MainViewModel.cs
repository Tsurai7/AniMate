using AniMate_app.Anilibria;
using AniMate_app.Model;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModel
{
    class MainViewModel : BindableObject
    {
        public ObservableCollection<GenreCollection> TitlesByGenre { get; private set; } = new();

        public List<string> Genres { get; private set; }

        public int GenresLoaded { get; private set; } = 0;

        public bool AllGenresLoaded => GenresLoaded.Equals(Genres.Count);

        public bool IsLoaded { get; private set; } = true;

        public async Task LoadContent()
        {
            Genres = await AnilibriaAPI.GetGenres();

            TitlesByGenre.Clear();

            await LoadTitlesByGenre(5);
        }

        public ObservableCollection<string> TestString { get; private set; } = new ObservableCollection<string>() { "test", "test" , "test", "test", "test", "test" };

        public void LoadMoreTest()
        {
            TestString.Add("bob");

            OnPropertyChanged(nameof(TestString));
        }

        public async Task LoadTitlesByGenre(int count)
        {
            if(!IsLoaded) return;

            IsLoaded = false;

            count = GenresLoaded + count < Genres.Count ? GenresLoaded + count : Genres.Count;

            for (int i = GenresLoaded; i < count; i++)
                TitlesByGenre.Add(new(Genres[i], await AnilibriaAPI.GetTilesByGenre(Genres[i])));

            GenresLoaded = count;

            IsLoaded = true;
        }
    }
}
