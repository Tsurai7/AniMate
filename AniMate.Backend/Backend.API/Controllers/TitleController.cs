using Backend.Application.Models.Title;
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

    [HttpGet("updates")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TitleDto>>> GetTitles(
        [FromQuery] int skip,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var titles = await _mediator.Send(new GetTitleListQueryParams(skip, limit), cancellationToken);
        return Ok(titles);
    }
}
