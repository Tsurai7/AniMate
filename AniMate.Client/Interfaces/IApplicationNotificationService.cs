using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniMate_app.Interfaces
{
    public interface IApplicationNotificationService
    {
        void SendRoomNotification(string title, string desc, string roomCode);
        void SendNotification(string title, string desc, bool isConstant);
    }
}
