using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AniMate_app.DTOs.Anime;

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
        Titles.Add(titleDto);
    }

    public void AddTitleList(IEnumerable<TitleDto> titles)
    {
        foreach (var title in titles)
        {
            if (title is not null)
                  AddTitle(title);
        }
        
    }

    public void Clear()
    {
        Titles.Clear();

        TargetTitleCount = 0;
    }
}