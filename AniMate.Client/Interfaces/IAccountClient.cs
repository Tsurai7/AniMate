using AniMate_app.DTOs.Auth;
using AniMate_app.Models;

namespace AniMate_app.Interfaces;

public interface IAccountClient
{
    Task<Profile> GetProfileInfo(string token);
    Task<bool> AddTitleToLiked(string token, string titleCode);
    Task<bool> RemoveTitleFromLiked(string token, string titleCode);
    Task<AuthResponse> SignIn(string email, string password);
    Task<bool> AddTitleToHistory(string token, string titleCode);
    Task<AuthResponse> SignUp(string email, string password, string username);
}