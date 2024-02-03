using AniMate_app.Anilibria;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AniMate_app.Model
{
    public class GenreCollection : ObservableObject
    {
        public string GenreName { get; private set; }

        public ObservableCollection<TitleRequestDto> Titles { get; private set; } 

        public int TargetTitleCount { get; set; }

        public int TitleCount => Titles.Count;

        public GenreCollection(string name)
        {
            GenreName = name;
            Titles = new();
        }

        public void AddTitle(TitleRequestDto title)
        {
            Titles.Add(title);
        }

        public void AddTitleList(IEnumerable<TitleRequestDto> titles)
        {
            foreach (var title in titles)
                AddTitle(title);
        }
    }
}
