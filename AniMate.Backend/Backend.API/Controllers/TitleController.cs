using Backend.API.Controllers.Models.Titles;
using Backend.Application.Handlers.Title;
using Backend.Domain.Models.Anime;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/title")]
public class TitleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TitleController> _logger;

    public TitleController(IMediator mediator, ILogger<TitleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("random")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TitleDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> GetRandomTitle(CancellationToken cancellationToken)
    {
        var title = await _mediator.Send(new GetRandomTitleRequest(),cancellationToken);

        var titleResponse = title.Adapt<Title>();

        return Ok(titleResponse);
    }

    [HttpGet("updates")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> GetUpdates(
        [FromQuery] int skip,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var titles = await _mediator.Send(new GetTitlesUpdatesQueryParams(skip, limit), cancellationToken);

        var titlesResponse = titles.Adapt<List<Title>>();

        return Ok(titlesResponse);
    }

    [HttpGet("search")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> SearchTitles(
        [FromQuery] int skip,
        [FromQuery] List<string>? genres,
        [FromQuery] string? orderBy,
        [FromQuery] int sortDirection,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var searchParams = new SearchTitleListQueryParams(
            skip,
            genres,
            orderBy,
            sortDirection,
            limit);
        
        var titles = await _mediator.Send(searchParams, cancellationToken);
        
        var titlesResponse = titles.Adapt<List<Title>>();

        return Ok(titlesResponse);
    }
}