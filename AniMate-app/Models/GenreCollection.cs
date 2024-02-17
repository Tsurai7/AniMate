using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AniMate_app.Model
{
    public class GenreCollection(string name) : ObservableObject
    {
        public string GenreName { get; private set; } = name;

        public ObservableCollection<Title> Titles { get; private set; } = new();

        public int TargetTitleCount { get; set; }

        public int TitleCount => Titles.Count;

        public void AddTitle(Title title)
        {
            Titles.Add(title);
        }

        public void AddTitleList(IEnumerable<Title> titles)
        {
            foreach (var title in titles)
                AddTitle(title);
        }

        public void Clear()
        {
            Titles.Clear();

            TargetTitleCount = 0;
        }
    }
}
