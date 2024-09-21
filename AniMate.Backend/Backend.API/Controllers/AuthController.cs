using Backend.Application.DTOs.Requests;
using Backend.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            var result = await _authService.SignUp(request);
            return Ok(new { Success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
    
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var token = await _authService.SignIn(request);
            return Ok(token);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}