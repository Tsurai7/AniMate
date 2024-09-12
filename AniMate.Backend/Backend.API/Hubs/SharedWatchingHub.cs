using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Backend.API.Hubs
{
    public class SharedWatchingHub : Hub
    {
        private static readonly ConcurrentDictionary<string, Room> Rooms = new();
        
        public async Task CreateRoom(string link, string titleCode, string episodeUrl)
        {
            var room = new Room
            {
                TitleCode = titleCode,
                Url = episodeUrl
            };
            
            room.Clients.Add(Context.ConnectionId);

            Rooms.GetOrAdd(link, room);

            await Groups.AddToGroupAsync(Context.ConnectionId, link);
            
            await Clients.Caller.SendAsync("CreatedRoom", link, titleCode, episodeUrl);
        }

        public async Task JoinRoom(string link)
        {
            if (Rooms.TryGetValue(link, out var roomToJoin))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, link);
                await Clients.OthersInGroup(link).SendAsync("SyncState", roomToJoin.Url, roomToJoin.CurrentTiming.TotalSeconds, roomToJoin.IsPlaying);
                return;
            }
            
            await Clients.Caller.SendAsync("Error", $"Failed to join room: {link}");
        }
        
        public async Task Pause(string roomName, double currentTiming)
        {
            try
            {
                if (Rooms.TryGetValue(roomName, out var room))
                {
                    room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
                    room.IsPlaying = false;
                }
                
                await Clients.OthersInGroup(roomName).SendAsync("Pause", currentTiming);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", "Failed to pause: " + ex.Message);
            }
        }
        
        public async Task Resume(string roomName, double currentTiming)
        {
            try
            {
                if (Rooms.TryGetValue(roomName, out var room))
                {
                    room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
                    room.IsPlaying = true;
                }
                
                await Clients.OthersInGroup(roomName).SendAsync("Resume", currentTiming);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", "Failed to resume: " + ex.Message);
            }
        }
        
        public async Task Seek(string roomName, double newTime)
        {
            try
            {
                if (Rooms.TryGetValue(roomName, out var room))
                {
                    room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, newTime));
                }
                
                await Clients.OthersInGroup(roomName).SendAsync("Seek", newTime);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", "Failed to seek: " + ex.Message);
            }
        }
        
        public async Task UpdateVideoUrl(string roomName, string newVideoUrl)
        {
            try
            {
                if (Rooms.TryGetValue(roomName, out var room))
                {
                    room.Url = newVideoUrl;
                    room.CurrentTiming = TimeSpan.Zero;
                    room.IsPlaying = false; 
                    
                    await Clients.Group(roomName).SendAsync("VideoUrlUpdated", newVideoUrl);
                    
                    await Clients.Group(roomName).SendAsync("SyncState", newVideoUrl, room.CurrentTiming.TotalSeconds, room.IsPlaying);
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", "Failed to update video URL: " + ex.Message);
            }
        }
        
        public async Task SyncStateForNewClient(string roomName)
        {
            if (Rooms.TryGetValue(roomName, out var room))
            {
                await Clients.Caller.SendAsync("SyncState", room.Url, room.CurrentTiming.TotalSeconds, room.IsPlaying);
            }
        }
        
        public async Task SendMessage(string roomName, string message)
        {
            var user = Context.ConnectionId;
            
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }
    }

    public class Room
    {
        public string TitleCode { get; set; } = string.Empty;
        
        public string Url { get; set; } = string.Empty;
        
        public TimeSpan CurrentTiming { get; set; } = TimeSpan.Zero;
        
        public bool IsPlaying { get; set; }
        
        public ConcurrentBag<string> Clients { get; } = [];
    }
}
