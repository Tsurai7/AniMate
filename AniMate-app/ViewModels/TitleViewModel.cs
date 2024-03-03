using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Title), "TheTitle")]
    [QueryProperty(nameof(TitleCode), "TitleCode")]
    public partial class TitleViewModel : ObservableObject
    {
        private AnilibriaService _service;

        public TitleViewModel(AnilibriaService anilibriaService)
        {
            _service = anilibriaService;
        }

        private Title _title;
        public Title Title
        {
            get => _title;
            set
            {
                _title = value;
                Genres = string.Join(", ", _title.Genres);
                ShortDescription = string.Join(" ", _title.RuDescription.Split(' ').Take(7));
                OnPropertyChanged(nameof(Title));
            }
        }

        public string TitleCode
        {
            set
            {
                LoadTitleFromCode(value);
            }
        }

        private async void LoadTitleFromCode(string code)
        {
            Title = await _service.GetTitleByCode(code);
        }

        [ObservableProperty]
        private string _genres;

        [ObservableProperty]
        private string _shortDescription;
    }
}
