using AniMate_app.Anilibria;
using System.Collections.ObjectModel;

namespace AniMate_app.Model
{
    public class GenreCollection
    {
        public string GenreName { get; private set; }

        public ObservableCollection<TitleRequestDto> Titles { get; private set; } 

        public int TitleCount => Titles.Count;

        public GenreCollection(string name, List<TitleRequestDto> titles)
        {
            GenreName = name;
            Titles = new ObservableCollection<TitleRequestDto>(titles);
        }
    }
}
