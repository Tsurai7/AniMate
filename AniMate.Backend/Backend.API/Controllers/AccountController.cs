using AutoMapper;
using Backend.API.Controllers.Models.Account;
using Backend.Application;
using Backend.Application.Handlers;
using Backend.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public AccountController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<GetAccountResponse> GetAccount(CancellationToken token)
    {
        var command = new GetAccountRequest();
        
        return await _mediator.Send(command, token);
    }
    
    [HttpPost("sign-up")]
    public async Task<AuthToken> SignUpAsync(
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
    public async Task<AuthToken> SignInAsync(
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
    
    [Authorize]
    [HttpPatch("{email}")]
    public async Task<IActionResult> UpdateProfile(
        [FromRoute] string email,
        [FromBody] JsonPatchDocument<UpdateAccountRequest> patchDocument,
        CancellationToken token)
    {
        var command = new UpdateAccountCommand
        {
            Email = email,
            PatchDocument = patchDocument
        };

        await _mediator.Send(command, token);
        
        return NoContent();
    }
}