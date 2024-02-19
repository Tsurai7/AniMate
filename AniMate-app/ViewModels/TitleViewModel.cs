using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Title), "TheTitle")]
    public partial class TitleViewModel : ObservableObject
    {
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

        private string _genres;
        public string Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                _shortDescription = value;
                OnPropertyChanged(nameof(ShortDescription));
            }
        }

        public TitleViewModel()
        {

        }
    }
}
