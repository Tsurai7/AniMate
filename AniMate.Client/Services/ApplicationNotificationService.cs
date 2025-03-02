using AniMate_app.Clients;
using AniMate_app.Utils;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
using Plugin.LocalNotification.iOSOption;

namespace AniMate_app.Services
{
    public class ApplicationNotificationService
    {
        private const string NOTIFICATION_SUBTITLE = "Animate";

        private const string ANDROID_CHANNELID = "animate";

        private int _notificationId = 0;

        private readonly LinkedList<int> _constantNotificationList = new();
        
        private readonly INotificationService _notificationCenter;

        private readonly SharedWatchingClient _sharedWatchingClient;

        public ApplicationNotificationService(INotificationService notificationCenter, SharedWatchingClient sharedWatchingClient)
        {
            _notificationCenter = notificationCenter;

            _sharedWatchingClient = sharedWatchingClient;

            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
        }

        ~ApplicationNotificationService()
        {
            LocalNotificationCenter.Current.NotificationActionTapped -= OnNotificationActionTapped;
        }

        public int SendNotification(string title, string desc, bool isConstant, bool ongoing = false, string returnData = "")
        {
            throw new NotImplementedException();
        }

        public int SendRoomNotification(string title, string desc, string roomCode)
        {
            int newNotificationId = _notificationId++;

            NotificationRequest notification = new NotificationRequest
            {
                NotificationId = newNotificationId,
                CategoryType = NotificationCategoryType.Progress,
                Title = title,
                Silent = true,
                Description = desc,
                ReturningData = roomCode,
                Subtitle = NOTIFICATION_SUBTITLE,
                Android = new()
                {
                    ChannelId = ANDROID_CHANNELID,
                    LaunchAppWhenTapped = true,
                    PendingIntentFlags = AndroidPendingIntentFlags.Immutable,
                    Ongoing = true,
                    AutoCancel = false,
                    VisibilityType = AndroidVisibilityType.Public,
                    Priority = AndroidPriority.Min,
                    IconLargeName = new()
                    {
                        ResourceName = "notificationlarge"
                    },
                    IconSmallName = new()
                    {
                        ResourceName = "sharewatch"
                    }
                },
                iOS = new()
                {
                    HideForegroundAlert = true,
                    Priority = iOSPriority.Passive,
                    RelevanceScore = 1,
                    ApplyBadgeValue = true
                }
            };

            _constantNotificationList.AddLast(newNotificationId);

            SendNotification(notification);

            return newNotificationId;
        }

        public void DeleteNotification(int id)
        {
            _constantNotificationList.Remove(id);

            _notificationCenter.Cancel(id);
        }

        public void ClearConstantNotifications()
        {
            _constantNotificationList.Map((id) => _notificationCenter.Cancel(id));

            _constantNotificationList.Clear();
        }

        private async void SendNotification(NotificationRequest request)
        {
            if (await _notificationCenter.AreNotificationsEnabled() == false)
                await _notificationCenter.RequestNotificationPermission();

            await _notificationCenter.Show(request);
        }

        private void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            switch (e.ActionId)
            {
                case 101:
                    _sharedWatchingClient.RequestDisconnect();
                    break;
            }

            if (e.IsDismissed)
            {
                if (_constantNotificationList.Contains(e.Request.NotificationId))
                    SendNotification(e.Request);

                return;
            }
            if (e.IsTapped)
            {

                return;
            }
        }
    }
}
