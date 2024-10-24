using Backend.API.Controllers.Models.Account;
using Backend.API.Controllers.Models.Account.Responses;
using Backend.Application.Handlers;
using Backend.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : Controller
{
    private readonly Mediator _mediator;
    public AccountController(Mediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("sign-up")]
    public async Task<AuthToken> SignUp(
        [FromBody] SignUpRequest request,
        CancellationToken token)
    {
        var command = new SignUpAccountCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        return await _mediator.Send(command, token);
    }
    
    [HttpPost("sign-in")]
    public async Task<AuthToken> SignIn(
        [FromBody] SignInRequest request,
        CancellationToken token)
    {
        var command = new SignInAccountCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        return await _mediator.Send(command, token);
    }
    
    [HttpGet("profile")]
    public async Task<ProfileDto> GetProfile()
    {
        return new ProfileDto("Nikita Desuyo", 
            "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.instagram.com%2Fmeguminfushiguro%2F&psig=AOvVaw3lG69Vz1JTkbsA8WZK9ZIz&ust=1726985201087000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCMC-srWv04gDFQAAAAAdAAAAABAE",
            "nikita@gmail.com", ["Naruto", "Jujutsu Kaisen"], ["Jujutsu Kaisen"]);
    }
}