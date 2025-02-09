#if ANDROID
using Android.Content.PM;
using Android.Views;
using AniMate_app.Interfaces;

namespace AniMate_app
{ 
    public class AndroidFullScreenService : IFullScreenService
    {
        private ScreenOrientation _savedOrientation;

        public AndroidFullScreenService()
        {
            var activity = Platform.CurrentActivity;

            _savedOrientation = activity != null ? activity.RequestedOrientation : ScreenOrientation.Portrait;
        }

        public void EnterFullScreen()
        {
            GoToOrientation(ScreenOrientation.Landscape, false);
        }

        public void ExitFullScreen()
        {
            GoToOrientation(ScreenOrientation.Portrait, true);
        }

        public void RestoreOriginal()
        {
            GoToOrientation(_savedOrientation, true);
        }

        private void GoToOrientation(ScreenOrientation orientation, bool navigationVisible)
        {
            var activity = Platform.CurrentActivity;
            if (activity == null)
                return;

            activity.Window.DecorView.SystemUiFlags = navigationVisible ? SystemUiFlags.Visible :
                    SystemUiFlags.HideNavigation |
                    SystemUiFlags.Fullscreen |
                    SystemUiFlags.ImmersiveSticky;

            if(orientation == ScreenOrientation.Landscape)
                activity.Window.AddFlags(WindowManagerFlags.Fullscreen);
            else
                activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);

            activity.RequestedOrientation = orientation;
        }
    }
}
#endif