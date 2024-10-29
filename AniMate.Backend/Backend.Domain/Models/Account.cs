using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Domain.Models;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ImageUrl { get; set; }
    public List<string>? LikedTitles { get; set; }
    public List<string>? WatchedTitles { get; set; }
}