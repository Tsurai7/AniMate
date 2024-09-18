using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Backend.API.Hubs
{
    public class SharedWatchingHub : Hub
    {
        private static readonly ConcurrentDictionary<string, Room> Rooms = new();
        private readonly ILogger<SharedWatchingHub> _logger;

        public SharedWatchingHub(ILogger<SharedWatchingHub> logger)
        {
            _logger = logger;
        }
        
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

            await SendMessage(link, $"Room created with name {link}");
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
            
            await SendMessage(link, $"New user joined");
        }
        
        public async Task Pause(string roomName, double currentTiming)
        {
            if (Rooms.TryGetValue(roomName, out var room))
            {
                room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
                room.IsPlaying = false;
            }
            
            await Clients.OthersInGroup(roomName).SendAsync("Pause", currentTiming);
            
            await SendMessage(roomName, $"Video paused on timing: {currentTiming}");
        }
        
        public async Task Resume(string roomName, double currentTiming)
        {
            if (Rooms.TryGetValue(roomName, out var room))
            {
                room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
                room.IsPlaying = true;
            }
            
            await Clients.OthersInGroup(roomName).SendAsync("Resume", currentTiming);
            await SendMessage(roomName, $"Video resumed on timing: {currentTiming}");
        }
        
        public async Task Seek(string roomName, double newTime)
        {
            if (Rooms.TryGetValue(roomName, out var room))
            {
                room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, newTime));
            }
            
            await Clients.OthersInGroup(roomName).SendAsync("Seek", newTime);
            
            await SendMessage(roomName, $"Video seeked on timing: {newTime}");
        }
        
        public async Task UpdateVideoUrl(string roomName, string newVideoUrl)
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
        
        public async Task SyncStateForNewClient(string roomName)
        {
            if (Rooms.TryGetValue(roomName, out var room))
            {
                await Clients.Caller.SendAsync("SyncState", room.Url, room.CurrentTiming.TotalSeconds, room.IsPlaying);
            }
        }
        
        public async Task SendMessage(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", message);
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
