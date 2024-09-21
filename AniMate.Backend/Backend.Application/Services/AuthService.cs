using Backend.Application.DTOs.Requests;
using Backend.Domain.Interfaces;
using Backend.Domain.Models;

namespace Backend.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthToken> SignUp(SignUpRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }
        
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Password = request.Passsword
        };
        
        var token = _tokenService.GenerateToken(user);
        return token;
    }

    public async Task<AuthToken> SignIn(SignInRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }
        
        var token = _tokenService.GenerateToken(user);
        return token;
    }
}