using AniMate_app.Clients;
using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;
using AniMate_app.Models;
using AniMate_app.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AniMate_app.ViewModels;

public partial class UpdatesViewModel : ViewModelBase
{
    private readonly AnimateClient _animateClient;
    
    [ObservableProperty]
    private GenreCollection _titles = new("updates");

    [ObservableProperty]
    private string _labelText;

    [ObservableProperty]
    private GenreCollection _resumeWatchList = new("resume");

    public UpdatesViewModel(AnimateClient animateClient)
    {
        _animateClient = animateClient;
        _loadMoreContentOffset = 3;
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

        var newTitles = await _animateClient.GetUpdates(Titles.TitleCount, _loadMoreContentOffset);

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
        var title = await _animateClient.GetRandomTitle();

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

