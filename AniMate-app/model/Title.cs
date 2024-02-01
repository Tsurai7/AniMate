using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.Model
{
    public partial class Title : ObservableObject
    {
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public string image;
        [ObservableProperty]
        public List<string> genre;
    }
}
