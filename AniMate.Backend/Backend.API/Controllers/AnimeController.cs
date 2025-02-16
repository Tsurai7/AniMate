using Backend.Application.Models.Title;
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
        _logger.LogInformation("Attempting to get title with code: {Code}", queryParams.Code);
        
        try
        {
            var command = new GetTitleQueryParams
            {
                Code = queryParams.Code,
            };
            
            var response = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully retrieved title with code: {Code}", queryParams.Code);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving title with code: {Code}", queryParams.Code);
            return Problem();
        }
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<List<TitleDto>>> GetTitleList(
        [FromQuery] GetTitleListQueryParams queryParams,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting to get title list");
        
        try
        {
            var command = new GetTitleListQueryParams
            {
                // Здесь можно добавить параметры, если они есть
            };
            
            var response = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully retrieved title list.");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving title list");
            return Problem();
        }
    }
    
    [HttpGet("latest")]
    public async Task<ActionResult<List<TitleDto>>> GetLatest(
        [FromQuery] GetTitleListQueryParams queryParams,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting to get title list");
        
        try
        {
            var command = new GetTitleListQueryParams
            {
                // Здесь можно добавить параметры, если они есть
            };
            
            var response = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully retrieved title list.");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving title list");
            return Problem();
        }
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<List<TitleDto>>> SearchTitles(
        [FromQuery] SearchTitlesQueryParams queryParams,
        CancellationToken token)
    {
        _logger.LogInformation("Attempting to search titles with parameters: {@QueryParams}", queryParams);
        
        try
        {
            var command = new SearchTitlesQueryParams
            {
                // Здесь можно добавить параметры, если они есть
            };
            
            var response = await _mediator.Send(command, token);
            _logger.LogInformation("Successfully searched titles.");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching titles with parameters: {@QueryParams}", queryParams);
            return Problem();
        }
    }
}
