using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AniMate_app.DTOs.Anime;
using AniMate_app.Utils;

namespace AniMate_app.Models;

public class GenreCollection(string name) : ObservableObject
{
    public string GenreName { get; private set; } = name;

    public ObservableCollection<TitleDto> Titles { get; private set; } = new();

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

    public void AddTitle(TitleDto titleDto)
    {
        if(titleDto != null)
            Titles.Add(titleDto);
    }

    public void AddTitleList(IEnumerable<TitleDto> titles)
    {
        titles.Map(title => AddTitle(title));
    }

    public void Clear()
    {
        Titles.Clear();

        TargetTitleCount = 0;
    }
}