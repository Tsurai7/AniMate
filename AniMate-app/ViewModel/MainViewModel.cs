using AniMate_app.Anilibria;
using AniMate_app.Model;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModel
{
    class MainViewModel : BindableObject
    {
        #region на будущее, не трогать до изменения макета!

        public ObservableCollection<GenreCollection> TitlesByGenre { get; private set; } = new();

        #endregion



        public ObservableCollection<string> Genres { get; private set; }

        public async Task LoadContent()
        {
            Genres = await AnilibriaAPI.GetGenres();

            OnPropertyChanged(nameof(Genres));

            await LoadTitlesByGenre();
        }

        private async Task LoadTitlesByGenre()
        {
            foreach (string genre in Genres)
            {
                TitlesByGenre.Add(new(genre, await AnilibriaAPI.GetTilesByGenre(genre)));
            }
        }
    }
}
