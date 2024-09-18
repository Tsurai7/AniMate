using System.Security.Authentication;
using AccountService.Application.DTOs;
using Backend.Domain.Models;
using AccountService.Infrastructure.Repositories;

namespace AccountService.Application.Services;

public class AuthService
{
    private readonly TokenService _tokenService;

    private readonly UserRepository _userRepository;

    public AuthService(TokenService tokenService, UserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<AuthResponse> SignIn(SignInRequest signInRequest)
    {
        var userInDb = await _userRepository.GetByEmail(signInRequest.Email);

        if (userInDb is null || (userInDb.PasswordHash != signInRequest.Password))
            throw new InvalidCredentialException();
        
        var accessToken = _tokenService.BuildToken(signInRequest.Email);
        
         return new AuthResponse(
            AccessToken: accessToken, 
            RefreshToken: accessToken);
    }

    public async Task<AuthResponse> SignUp(SignUpRequest signUpRequest)
    {
        var userInDb = await _userRepository.GetByEmail(signUpRequest.Email);
        
        if (userInDb != null)
            throw new ArgumentException($"Пользователь с email {signUpRequest.Email} уже существует.");
        
        var newUser = new User(
            signUpRequest.Username,
            signUpRequest.Email,
            signUpRequest.Password,
            "img",
            new List<string>(),
            new List<string>());
        
        await _userRepository.AddAsync(newUser);
        
        var accessToken = _tokenService.BuildToken(newUser.Email);
        var refreshToken = _tokenService.BuildToken(newUser.Email);

        return new AuthResponse(
            accessToken,
            refreshToken);
    }
}