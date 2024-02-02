using AniMate_app.Anilibria;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AniMate_app.Model
{
    public class GenreCollection : ObservableObject
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
