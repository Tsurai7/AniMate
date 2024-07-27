namespace Application.DTOs;

public record AuthResponse
(
    string AccessToken,
    string RefreshToken
);