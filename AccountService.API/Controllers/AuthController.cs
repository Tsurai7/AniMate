using System.Security.Authentication;
using AccountService.Application.DTOs;
using AccountService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

[Controller]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
    {
        try
        {
            var authResponse = await _authService.SignIn(signInRequest);
            return Ok(authResponse);
        }
        
        catch (InvalidCredentialException ex)
        {
            return Unauthorized(ex.Message);
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("signUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        try
        {
            var authResponse = await _authService.SignUp(signUpRequest);
            return Ok(authResponse);
        }
        
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}