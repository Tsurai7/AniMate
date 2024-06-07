using Newtonsoft.Json;

namespace AniMate_app.Services.AccountService.Dtos;

public record AuthResponse
{
    [JsonProperty("accessToken")]
    public string? AccessToken { get; set; }
    
    [JsonProperty("refreshToken")]
    public string? RefreshToken { get; set; }
}