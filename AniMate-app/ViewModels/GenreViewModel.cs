using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModels;

public partial class GenreViewModel : ObservableObject
{
    public string Genre { get; set; }
    public ObservableCollection<Title> Titles { get; private set; }

    private readonly AnilibriaService _anilibriaService;

    private bool _isLoading = false;

    private int LoadedTitles;

    private int _loadMoreResultsOffset = 6;

    public GenreViewModel(string genre, ObservableCollection<Title> titles, AnilibriaService anilibriaService)
    {
        Titles = titles;

        Genre = genre;

        _anilibriaService = anilibriaService;

        LoadedTitles = titles.Count;
    }

    [RelayCommand]
    public async Task LoadMoreTitles()
    {
        if (_isLoading)
            return;

        _isLoading = true;

        foreach (var title in await _anilibriaService.GetTitlesByGenre(Genre, skip: LoadedTitles, LoadedTitles + _loadMoreResultsOffset))
            Titles.Add(title);

        _isLoading = false;
    }
}