using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Backend.Domain.Models;

namespace Backend.API.Hubs;

public class SharedWatchingHub : Hub
{
    private readonly IMemoryCache _cache;
    private const int RoomExpirationMinutes = 120;

    public SharedWatchingHub(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task CreateRoom(string titleCode, string episodeUrl)
    {
        var roomId = GenerateRoomId();
        
        var room = new Room
        {
            Id = roomId,
            TitleCode = titleCode,
            MediaUrl = episodeUrl
        };

        room.Clients.Add(Context.ConnectionId);
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(RoomExpirationMinutes));

        _cache.Set(roomId, room, cacheOptions);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await Clients.Caller.SendAsync("CreatedRoom", roomId, titleCode, episodeUrl);

        await SendMessage(roomId, $"Room created with name {roomId}");
    }

    public async Task JoinRoom(string roomId)
    {
        if (_cache.TryGetValue(roomId, out Room roomToJoin))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.OthersInGroup(roomId).SendAsync("SyncState", roomToJoin.MediaUrl, roomToJoin.CurrentTiming.TotalSeconds, roomToJoin.IsPlaying);
            
            await SendMessage(roomId, $"New user joined");
            return;
        }

        await Clients.Caller.SendAsync("Error", $"Failed to join room: {roomId}");
    }

    public async Task Pause(string roomId, double currentTiming)
    {
        if (_cache.TryGetValue(roomId, out Room room))
        {
            room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
            room.IsPlaying = false;
            _cache.Set(roomId, room);
        }

        await Clients.OthersInGroup(roomId).SendAsync("Pause", currentTiming);
        await SendMessage(roomId, $"Video paused on timing: {currentTiming}");
    }

    public async Task Resume(string roomId, double currentTiming)
    {
        if (_cache.TryGetValue(roomId, out Room room))
        {
            room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, currentTiming));
            room.IsPlaying = true;
            _cache.Set(roomId, room);
        }

        await Clients.OthersInGroup(roomId).SendAsync("Resume", currentTiming);
        await SendMessage(roomId, $"Video resumed on timing: {currentTiming}");
    }

    public async Task Seek(string roomId, double newTime)
    {
        if (_cache.TryGetValue(roomId, out Room room))
        {
            room.CurrentTiming = TimeSpan.FromSeconds(Math.Max(0, newTime));
            _cache.Set(roomId, room);
        }

        await Clients.OthersInGroup(roomId).SendAsync("Seek", newTime);
        await SendMessage(roomId, $"Video seeked on timing: {newTime}");
    }

    public async Task UpdateVideoUrl(string roomId, string newVideoUrl)
    {
        if (_cache.TryGetValue(roomId, out Room room))
        {
            room.MediaUrl = newVideoUrl;
            room.CurrentTiming = TimeSpan.Zero;
            room.IsPlaying = false;
            _cache.Set(roomId, room);

            await Clients.Group(roomId).SendAsync("VideoUrlUpdated", newVideoUrl);
            await Clients.Group(roomId).SendAsync("SyncState", newVideoUrl, room.CurrentTiming.TotalSeconds, room.IsPlaying);
        }
    }

    public async Task SyncStateForNewClient(string roomId)
    {
        if (_cache.TryGetValue(roomId, out Room room))
        {
            await Clients.Caller.SendAsync("SyncState", room.MediaUrl, room.CurrentTiming.TotalSeconds, room.IsPlaying);
        }
    }

    public async Task SendMessage(string roomId, string message)
    {
        await Clients.Group(roomId).SendAsync("ReceiveMessage", message);
    }
    
    private string GenerateRoomId() 
        => Guid.NewGuid().ToString()[..8];
}
