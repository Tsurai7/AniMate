using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(Genre), "GenreName")]
public partial class GenreViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _genre;

    [ObservableProperty]
    private GenreCollection _titlesCollection;
    
    private readonly IAnimeClient _animeClient;

    [ObservableProperty]
    private bool _isLoading = false;

    private int LoadedTitles => TitlesCollection.TitleCount;

    private readonly int _loadMoreResultsOffset = 6;

    public GenreViewModel(IAnimeClient animeClient)
    {
        _animeClient = animeClient;
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

        TitlesCollection.TargetTitleCount += _loadMoreResultsOffset;

        var loadedTitles = await _animeClient.GetTitlesByGenre(Genre, LoadedTitles, LoadedTitles + _loadMoreResultsOffset);

        if (loadedTitles.Count > 0)
            TitlesCollection.AddTitleList(loadedTitles);

        IsLoading = false;
    }
}