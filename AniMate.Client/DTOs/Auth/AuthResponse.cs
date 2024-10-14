namespace AniMate_app.DTOs.Auth;

public record AuthResponse
(
    string AccessToken,
    string RefreshToken
);