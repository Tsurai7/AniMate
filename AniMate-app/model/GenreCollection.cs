using AniMate_app.Anilibria;

namespace AniMate_app.Model
{
    public class GenreCollection
    {
        public string GenreName { get; private set; }

        public List<TitleRequestDto> Titles { get; private set; } 

        public int TitleCount => Titles.Count;

        public GenreCollection(string name, List<TitleRequestDto> titles)
        {
            GenreName = name;
            Titles = titles;
        }
    }
}
