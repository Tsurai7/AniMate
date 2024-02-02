using AniMate_app.Anilibria;

namespace AniMate_app.ViewModel
{
    public class PlayerViewModel : BindableObject
    {
        public TitleRequestDto Title {  get; private set; }

        public string Genres { get; private set; }

        public PlayerViewModel(TitleRequestDto currentTitle)
        {
            Title = currentTitle;

            Genres = string.Join(", ", Title.Genres);
        }
    }
}
