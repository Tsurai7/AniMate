using Newtonsoft.Json;

namespace AniMate_app.DTOs.Account;

public record ProfileDto
(
    [JsonProperty("username")] 
    string Username,
    
    [JsonProperty("profile_image")] 
    string ProfileImage,
    
    [JsonProperty("email")] 
    string Email,
    
    [JsonProperty("watched_titles")] 
    List<string> WatchedTitles,
    
    [JsonProperty("liked_titles")] 
    List<string> LikedTitles
);