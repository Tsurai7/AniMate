using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AniMate_app.Model
{
    public class GenreCollection(string name) : ObservableObject
    {
        public string GenreName { get; private set; } = name;

        public ObservableCollection<Title> Titles { get; private set; } = new();

        private int _targetTitleCount;

        public int TargetTitleCount
        {
            get => _targetTitleCount;
            set
            {
                if (_targetTitleCount != value)
                {
                    _targetTitleCount = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(TargetTitleCount)));
                }
            }
        }

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
