using AniMate_app.Services.AnilibriaService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.ViewModels
{
    public partial class UpdatesViewModel : ObservableObject
    {
        private AnilibriaService _anilibriaService;

        public UpdatesViewModel(AnilibriaService anilibriaService)
        {
            _anilibriaService = anilibriaService;
        }
    }
}
