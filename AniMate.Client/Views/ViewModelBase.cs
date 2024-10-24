using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AniMate_app.Views
{
    public abstract partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isRefreshing = false;

        protected int _loadMoreContentOffset = 5;

        public abstract Task LoadContent();

        public abstract Task LoadMoreContent();
    }
}
