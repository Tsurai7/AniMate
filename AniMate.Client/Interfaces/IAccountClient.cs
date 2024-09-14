using System.Threading.Tasks;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Auth;

namespace AniMate_app.Interfaces;

public interface IAccountClient
{
    Task<ProfileDto> GetProfileInfo(string token);
    Task<bool> AddTitleToLiked(string token, string titleCode);
    Task<bool> RemoveTitleFromLiked(string token, string titleCode);
    Task<AuthResponse> SignIn(string email, string password);
    Task<bool> AddTitleToHistory(string token, string titleCode);
    Task<AuthResponse> SignUp(string email, string password, string username);
}