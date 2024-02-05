using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
            Titles.Clear();

            if (string.IsNullOrEmpty(name)) 
                return;

            foreach (var title in await _anilibriaService.GetAllTitlesByName(name))
            {
                Titles.Add(title);
            }
        }
    }
}
