using System.Security.Claims;
using Backend.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Backend.Infrastructure.Repositories;

namespace Backend.Application.Services;

public class AccountService
{
    private readonly UserRepository _userRepository;

    public AccountService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ProfileInfo> GetProfile(HttpContext context)
    {
        string email = context.User.FindFirst(ClaimTypes.Name)?.Value;
            
        var userInDb = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Email == email);

        if (userInDb is not null)
            return new ProfileInfo(
                userInDb.UserName,
                userInDb.ProfileImage,
                userInDb.Email,
                userInDb.WatchedTitles,
                userInDb.LikedTitles);
            
        throw new KeyNotFoundException($"Пользователь с email '{email}' не найден в базе данных.");
    }
}