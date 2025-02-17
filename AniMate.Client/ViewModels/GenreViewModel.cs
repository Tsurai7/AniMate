using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(Genre), "GenreName")]
public partial class GenreViewModel : ViewModelBase
{
    private string _genre;

    public string Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            TitlesCollection = new(_genre);
            OnPropertyChanged(nameof(_genre));
        }
    }

    [ObservableProperty]
    private GenreCollection _titlesCollection;
    
    private readonly IAnimeClient _animeClient;

    [ObservableProperty]
    private bool _isLoading = false;

    private int LoadedTitles => TitlesCollection.TitleCount;

    public GenreViewModel(IAnimeClient animeClient)
    {
        _animeClient = animeClient;
        _loadMoreContentOffset = 4;
    }

    public override Task LoadContent()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    public override async Task LoadMoreContent()
    {
        if (IsLoading)
            return;

        if (TitlesCollection.TargetTitleCount > TitlesCollection.TitleCount)
            return;

        IsLoading = true;

        TitlesCollection.TargetTitleCount += _loadMoreContentOffset;

        var loadedTitles = await _animeClient.GetTitlesByGenre(Genre, LoadedTitles, LoadedTitles + _loadMoreContentOffset);

        if (loadedTitles.Count > 0)
            TitlesCollection.AddTitleList(loadedTitles);

        IsLoading = false;
    }
}