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
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public List<string> LikedTitles { get; set; } = [];
    public List<string> WatchedTitles { get; set; } = [];
}