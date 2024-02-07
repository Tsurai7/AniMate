using AniMate_app.Services.AnilibriaService.Models;

namespace AniMate_app.ViewModels
{
    public class PlayerViewModel : BindableObject
    {
        public Title Title {  get; private set; }

        public string Genres { get; private set; }

        public PlayerViewModel(Title currentTitle)
        {
            Title = currentTitle;

            Genres = string.Join(", ", Title.Genres);
        }
    }
}
