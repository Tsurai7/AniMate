using AutoMapper;
using Backend.Application.Handlers.Account;
using Backend.Application.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
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
    [HttpGet("profile")]
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
            _logger.LogWarning(ex, "Error fetching account details for user {email}", User.Identity?.Name);
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
