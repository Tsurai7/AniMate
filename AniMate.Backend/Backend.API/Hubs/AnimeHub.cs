using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class AnimeHub : Hub
{
    private static readonly Dictionary<string, Group> AnimeGroups = new();

    public async Task MakeWatchingGroup(string userName, EpisodeInfo episodeInfo)
    {
        Group animeGroup = GetOrCreateAnimeGroup(userName);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, animeGroup.Name);
        await Clients.Group(animeGroup.Name).SendAsync("UserJoined", Context.ConnectionId);
        
        foreach (var member in AnimeGroups)
        {
            Console.WriteLine(member.Value.Name);
        }
    }

    public async Task StartAnimeWatch(string animeId)
    {
        Group animeGroup = GetAnimeGroup(animeId);
        await Clients.Group(animeGroup.Name).SendAsync("StartAnimeWatch");
    }

    public async Task PauseAnimeWatch(string animeId)
    {
        Group animeGroup = GetAnimeGroup(animeId);
        await Clients.Group(animeGroup.Name).SendAsync("PauseAnimeWatch");
    }

    public async Task SeekAnimeWatch(string animeId, double position)
    {
        Group animeGroup = GetAnimeGroup(animeId);
        await Clients.Group(animeGroup.Name).SendAsync("SeekAnimeWatch", position);
    }

    private Group GetOrCreateAnimeGroup(string animeId)
    {
        if (!AnimeGroups.TryGetValue(animeId, out Group animeGroup))
        {
            animeGroup = new Group { Name = $"AnimeWatch:{animeId}" };
            AnimeGroups[animeId] = animeGroup;
        }

        return animeGroup;
    }

    private Group GetAnimeGroup(string animeId)
    {
        if (!AnimeGroups.TryGetValue(animeId, out Group animeGroup))
        {
            throw new ArgumentException($"Группа для просмотра аниме с ID '{animeId}' не найдена.");
        }

        return animeGroup;
    }

    public record EpisodeInfo(string TitleCode, string Ordinal);
    private class Group
    {
        public string Name { get; set; }
    }
}