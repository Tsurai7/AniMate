using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModels;

public partial class GenreViewModel : ObservableObject
{
    public string Genre { get; set; }
    public ObservableCollection<Title> Titles { get; private set; }

    private readonly AnilibriaService _anilibriaService;

    private int LoadedTitles;

    public GenreViewModel(ObservableCollection<Title> titles, string genre, AnilibriaService anilibriaService)
    {
        Titles = titles;

        Genre = genre;

        _anilibriaService = anilibriaService;

        LoadedTitles = titles.Count;
    }


    private async void LoadMoreTitles()
    {
    }
}