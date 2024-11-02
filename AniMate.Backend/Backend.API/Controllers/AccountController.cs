using AutoMapper;
using Backend.API.Controllers.Models.Account;
using Backend.Application.Handlers;
using Backend.Application.Models;
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
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(
        IMediator mediator,
        IMapper mapper, 
        ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetAccountResponse>> GetAccount(CancellationToken token)
    {
        _logger.LogInformation("Fetching account details for user {email}", User.Identity?.Name);

        try
        {
            var command = new GetAccountRequest();
            var response = await _mediator.Send(command, token);
            
            _logger.LogInformation("Successfully fetched account details for user {email}", User.Identity?.Name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching account details for user {email}", User.Identity?.Name);
            return Problem();
        }
    }
    
    [HttpPost("sign-up")]
    public async Task<ActionResult<AuthToken>> SignUpAsync(
        [FromBody] SignUpRequest request,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-up for email {email}", request.Email);

        try
        {
            var command = new SignUpAccountCommand
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
            _logger.LogError(ex, "Error during sign-up for email {email}", request.Email);
            return Problem();
        }
    }
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<AuthToken>> SignInAsync(
        [FromBody] SignInRequest request,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting sign-in for email {email}", request.Email);

        try
        {
            var command = new SignInAccountCommand
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
            return Problem();
        }
    }
    
    [Authorize]
    [HttpPatch("{email}")]
    public async Task<IActionResult> UpdateProfile(
        [FromRoute] string email,
        [FromBody] JsonPatchDocument<UpdateAccountRequest> patchDocument,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting to update profile for email {email}", email);

        try
        {
            
            var command = new UpdateAccountCommand
            {
                Email = email,
                PatchDocument = patchDocument
            };

            await _mediator.Send(command, token);
            
            _logger.LogInformation("Successfully updated profile for email {email}", email);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating profile for email {email}", email);
            return Problem();
        }
    }
}
