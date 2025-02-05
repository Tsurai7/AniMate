using System.Collections.Concurrent;

namespace Backend.Domain.Models;

public class Room
{
    public string Id { get; init; } = string.Empty;
    public string TitleCode { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public TimeSpan CurrentTiming { get; set; } = TimeSpan.Zero;
    public bool IsPlaying { get; set; }
    public ConcurrentBag<string> Clients { get; } = [];
}