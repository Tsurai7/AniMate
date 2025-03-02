using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.iOSOption;

namespace AniMate_app;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .UseLocalNotification(config =>
            {
                config.AddAndroid(android =>
                {
                    android.AddChannel(new NotificationChannelRequest
                    {
                        Id = $"animate",
                        Name = "AnimateAppChannel",
                        Description = "Animate App Channel",
                    });
                });
                config.AddCategory(new NotificationCategory(NotificationCategoryType.Progress)
                {
                    ActionList = new HashSet<NotificationAction>(new List<NotificationAction>()
                                {
                                    new(101)
                                    {
                                            Title = "Exit",
                                            Android =
                                            {
                                                LaunchAppWhenTapped = false,
                                                //IconName =
                                                //{
                                                //        ResourceName = "i3"
                                                //}
                                            },
                                            IOS =
                                            {
                                                    Action = iOSActionType.Destructive
                                            }
                                    }
                                })
                });
            })
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        
        builder.Services.ConfigureServices();
      
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}