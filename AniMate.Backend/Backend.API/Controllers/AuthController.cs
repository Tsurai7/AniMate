using Backend.Application.Models.Account;
using Backend.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(
        IMediator mediator,
        ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthToken>> SignUp(
        [FromBody] SignUpRequest request,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-up for email {email}", request.Email);

        try
        {
            var command = new SignUpRequest
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
            
            var tokenResponse = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully signed up user with email {email}", request.Email);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during sign-up for email {email}", request.Email);
            return Conflict(new { message = "An account with this email already exists." });
        }
    }
    
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthToken>> SignInAsync(
        [FromBody] SignInRequest request,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-in for email {email}", request.Email);

        try
        {
            var command = new SignInRequest
            {
                Email = request.Email,
                Password = request.Password
            };
            
            var tokenResponse = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully signed in user with email {email}", request.Email);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during sign-in for email {email}", request.Email);
            return Unauthorized(new { message = "Invalid credentials." });
        }
    }
    
    [HttpGet("refresh-token/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthToken>> RefreshToken(
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-in for email {email}");
        return Ok();
    }
    
    [HttpPost("sign-out/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthToken>> SignOut(
        [FromBody] SignInRequest request,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-in for email {email}", request.Email);
        return Ok();
    }
}