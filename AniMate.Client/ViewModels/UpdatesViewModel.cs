using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;

public partial class UpdatesViewModel : ViewModelBase
{
    private readonly IAnimeClient _animeClient;
    
    [ObservableProperty]
    private GenreCollection _titles = new("updates");

    [ObservableProperty]
    private string _labelText;

    [ObservableProperty]
    private GenreCollection _resumeWatchList = new("resume");

    public UpdatesViewModel(IAnimeClient animeClient)
    {
        _animeClient = animeClient;
    }

    public override async Task LoadContent()
    {
        await LoadMoreContent();
    }

    [RelayCommand]
    public override async Task LoadMoreContent()
    {
        if (IsLoading)
            return;

        IsLoading = true;

        var newTitles = await _animeClient.GetUpdates(Titles.TitleCount, _loadMoreContentOffset);

        Titles.AddTitleList(newTitles);
    
        IsLoading = false;
    }

    [RelayCommand]
    public async Task Refresh()
    {
        IsRefreshing = true;

        Titles.Clear();

        ResumeWatchList.Clear();

        await LoadMoreContent();

        IsRefreshing = false;
    }

    public async Task<TitleDto> GetRandomTitle()
    {
        TitleDto title = await _animeClient.GetRandomTitle();

        return title;
    }

    //public async Task LoadSavedData()
    //{
    //    if (Preferences.Default.ContainsKey("visited"))
    //    {
    //        var lastVisited = Preferences.Default.Get<string>("visited", default).Split(';');

    //        foreach (string code in lastVisited)
    //            ResumeWatchList.AddTitle(await _anilibriaService.GetTitleByCode(code));
    //    }
    //}
}

