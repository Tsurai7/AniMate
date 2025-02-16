namespace AniMate_app.DTOs.Auth;

public record AuthResponse
(
    string Token,
    DateTime Expiration
);