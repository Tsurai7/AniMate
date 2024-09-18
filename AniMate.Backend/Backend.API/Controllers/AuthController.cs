using Backend.Application.DTOs.Requests;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest model)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest model)
    {
        throw new NotImplementedException();
    }
}