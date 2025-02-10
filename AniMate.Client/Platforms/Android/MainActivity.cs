using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace AniMate_app
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/AppTheme.Starting", ResizeableActivity = true, LaunchMode = LaunchMode.SingleTask, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(
        [Intent.ActionView],
        Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable],
        DataScheme = "https",
        DataHost = "animate",
        DataPathPrefix = "/",
        AutoVerify = true
        )
    ]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);

            if (intent == null)
                return;

            ProcessIntent(intent);
        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                Window.SetDecorFitsSystemWindows(false);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Always;
                Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#1d2333"));
            }

            AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);

            base.OnCreate(savedInstanceState);

            if(Intent == null)
                return;

            ProcessIntent(Intent);

            Thread.Sleep(1000);
        }

        private void ProcessIntent(Intent intent)
        {
            var url = intent.DataString;

            if (string.IsNullOrEmpty(url))
                return;

            Microsoft.Maui.Controls.Application.Current?.SendOnAppLinkRequestReceived(new Uri(url));
        }
    }
}