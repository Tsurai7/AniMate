using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;
[QueryProperty(nameof(Genre), "GenreName")]
[QueryProperty(nameof(AnilibriaService), "AnilibriaService")]
public partial class GenreViewModel : ObservableObject
{
    private string _genre;
    public string Genre { get => _genre;
        set
        {
            _genre = value;
            TitlesCollection = new(Genre);
            OnPropertyChanged(nameof(TitlesCollection));
            OnPropertyChanged(nameof(Genre));
        }
    }
    public GenreCollection TitlesCollection { get; private set; }

    private AnilibriaService _anilibriaService;

    public AnilibriaService AnilibriaService {
        get => _anilibriaService;
        set
        {
            _anilibriaService = value;
            OnPropertyChanged(nameof(AnilibriaService));
        }
    }

    [ObservableProperty]
    private bool _isLoading;

    private int LoadedTitles => TitlesCollection.TitleCount;

    private int _loadMoreResultsOffset = 6;

    public GenreViewModel()
    {
        IsLoading = false;
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

        List<Title> loadedTitles = await AnilibriaService.GetTitlesByGenre(Genre, LoadedTitles, LoadedTitles + _loadMoreResultsOffset);

        if (loadedTitles.Count > 0)
            TitlesCollection.AddTitleList(loadedTitles);

        IsLoading = false;
    }
}