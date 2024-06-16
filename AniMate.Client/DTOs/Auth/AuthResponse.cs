using Newtonsoft.Json;

namespace AniMate_app.DTOs.Auth;

public record AuthResponse
(
    [JsonProperty("accessToken")]
    string AccessToken,

    [JsonProperty("refreshToken")]
    string RefreshToken
);