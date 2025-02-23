using Backend.Application.Handlers.Title;
using Backend.Domain.Models.Anime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/title")]
public class TitleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TitleController> _logger;

    public TitleController(
        IMediator mediator,
        ILogger<TitleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet("random")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TitleDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> GetRandomTitle(CancellationToken ctx)
    {
        var titles = await _mediator.Send(new GetRandomTitleRequest(),ctx);
        return Ok(titles);
    }

    [HttpGet("updates")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> GetUpdates(
        [FromQuery] int skip,
        [FromQuery] int limit,
        CancellationToken ctx)
    {
        var titles = await _mediator.Send(new GetTitlesUpdatesQueryParams(skip, limit), ctx);
        return Ok(titles);
    }
    
    [HttpGet("search")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> SearchTitles(
        [FromQuery] int skip,
        [FromQuery] List<string>? genres,
        [FromQuery] string orderBy,
        [FromQuery] int sortDirection,
        [FromQuery] int limit,
        CancellationToken ctx)
    {
        var titles = await _mediator.Send(new SearchTitleListQueryParams(
            skip,
            genres,
            orderBy,
            sortDirection,
            limit), ctx);
        return Ok(titles);
    }
}