using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Title), "TheTitle")]
    [QueryProperty(nameof(TitleCode), "TitleCode")]
    public partial class TitleViewModel : ObservableObject
    {
        private readonly AnilibriaService _service;

        private readonly Stack<string> _lastVisited;

        public TitleViewModel(AnilibriaService anilibriaService)
        {
            _service = anilibriaService;

            var lastVisited = Preferences.Default.Get<string>("visited", default);

            _lastVisited = lastVisited is null ? new() : new(lastVisited.Split(';'));
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

        public void OnNavigatedTo()
        {
            while(_lastVisited.Count > 4)
                _lastVisited.Pop();

            if(!_lastVisited.Contains(Title.Code))
                _lastVisited.Push(Title.Code);

            Preferences.Default.Set<string>("visited", string.Join(';', _lastVisited));
        }
    }
}
