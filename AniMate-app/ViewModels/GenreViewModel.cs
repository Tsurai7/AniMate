using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;

public partial class GenreViewModel : ObservableObject
{
    public string Genre { get; set; }
    public GenreCollection TitlesCollection { get; private set; }

    private readonly AnilibriaService _anilibriaService;

    [ObservableProperty]
    private bool _isLoading;

    private int LoadedTitles => TitlesCollection.TitleCount;

    private int _loadMoreResultsOffset = 6;

    public GenreViewModel(string genreName, AnilibriaService anilibriaService)
    {
        IsLoading = false;

        Genre = genreName;

        TitlesCollection = new(Genre);

        _anilibriaService = anilibriaService;
    }

    [RelayCommand]
    public async Task LoadMoreTitles()
    {
        if (IsLoading)
            return;

        if (TitlesCollection.TargetTitleCount > TitlesCollection.TitleCount)
            return;

        IsLoading = true;

        TitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

        List<Title> loadedTitles = await _anilibriaService.GetTitlesByGenre(Genre, LoadedTitles, LoadedTitles + _loadMoreResultsOffset);

        if (loadedTitles.Count > 0)
            TitlesCollection.AddTitleList(loadedTitles);

        IsLoading = false;
    }
}