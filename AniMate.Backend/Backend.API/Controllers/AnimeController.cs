using Backend.API.Controllers.Models.Title;
using Backend.Application.Handlers;
using Backend.Domain.Models.Anime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/anime")]
public class AnimeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(
        IMediator mediator,
        ILogger<AnimeController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<TitleDto>> GetTitle(
        [FromQuery] GetTitleQueryParams queryParams,
        CancellationToken token)
    {
        var command = new GetTitleCommand
        {
            Code = queryParams.Code,
        };
        
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
}