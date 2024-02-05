using AniMate_app.Model;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using AniMate_app.Services.AnilibriaService.Models;

namespace AniMate_app.ViewModel
{
    public partial class SearchViewModel : ObservableObject
    {
        public ObservableCollection<Title> Titles { get; private set; } = new();

        private readonly AnilibriaService _anilibriaService;

        public SearchViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;
        }

        public async void FindTitles(string name)
        {
            var titlesList = await _anilibriaService.GetAllTitlesByName(name);
            Titles = new ObservableCollection<Title>(titlesList);

        }
    }
}
