using Backend.Application.DTOs.Requests;
using Backend.Domain.Models;

namespace Backend.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthToken> SignUp(SignUpRequest request);
    Task<AuthToken> SignIn(SignInRequest request);
}