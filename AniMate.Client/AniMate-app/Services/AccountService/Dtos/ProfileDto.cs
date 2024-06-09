using Newtonsoft.Json;

namespace AniMate_app.Services.AccountService.Dtos;

public record ProfileDto
{
    [JsonProperty("username")] 
    public string Username { get; set; }
    
    [JsonProperty("profile_image")] 
    public string ProfileImage { get; set; }
    
    [JsonProperty("email")] 
    public string Email { get; set; }
    
    [JsonProperty("watched_titles")] 
    public List<string> WatchedTitles { get; set; }
    
    [JsonProperty("liked_titles")] 
    public List<string> LikedTitles { get; set; }
};