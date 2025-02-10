#if ANDROID
using Android.Content.PM;
using Android.Views;
using AniMate_app.Interfaces;

namespace AniMate_app
{ 
    public class AndroidScreenOrientationService : IScreenOrientationService
    {
        public void AllowFullScreen()
        {
            GoToOrientation(ScreenOrientation.Unspecified, false);
        }

        public void RestrictFullScreen()
        {
            GoToOrientation(ScreenOrientation.Portrait, true);
        }

        private void GoToOrientation(ScreenOrientation orientation, bool navigationVisible)
        {
            var activity = Platform.CurrentActivity;
            if (activity == null)
                return;

            activity.Window.DecorView.SystemUiFlags = navigationVisible ? SystemUiFlags.Visible | SystemUiFlags.LayoutFullscreen :
                    SystemUiFlags.HideNavigation | SystemUiFlags.LayoutFullscreen;

            //if (orientation == ScreenOrientation.Unspecified)
            //    activity.Window.AddFlags(WindowManagerFlags.LayoutInScreen);
            //else
            //    activity.Window.ClearFlags(WindowManagerFlags.LayoutInScreen);

            activity.RequestedOrientation = orientation;
        }
    }
}
#endif