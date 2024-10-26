using Backend.API.Controllers.Models.Account;
using Backend.Application.Handlers;
using Backend.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;
    
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("sign-up")]
    public async Task<AuthToken> SignUpAsync([FromBody] SignUpRequest request, CancellationToken token)
    {
        var command = new SignUpAccountCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        return await _mediator.Send(command, token);
    }
    
    [HttpPost("sign-in")]
    public async Task<AuthToken> SignInAsync([FromBody] SignInRequest request, CancellationToken token)
    {
        var command = new SignInAccountCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        return await _mediator.Send(command, token);
    }
    
    [Authorize]
    [HttpGet("profile")]
    public async Task<GetProfileResponse> GetProfile(CancellationToken token)
    {
        var command = new GetProfileRequest();
        
        return await _mediator.Send(command, token);
    }
    
    [HttpPut("titles/watched/{code}")]
    public async Task<ActionResult> AddToWatchedAsync(int code, CancellationToken token)
    {
        return Ok();
    }
    
    [HttpPut("titles/liked/{code}")]
    public async Task<ActionResult> AddToLikedAsync(int code, CancellationToken token)
    {
        return Ok();
    }
        
    [HttpPut("titles/liked/{code}")]
    public async Task<ActionResult> RemoveFromWatched(int code, CancellationToken token)
    {
        return Ok();
    }
}