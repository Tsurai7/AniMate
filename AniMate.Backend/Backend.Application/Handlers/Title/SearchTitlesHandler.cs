using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public record SearchTitleListQueryParams(
    int Skip,
    List<string>? genres,
    string OrderBy,
    int SortDirection,
    int Limit) : IRequest<List<TitleDto>>;

public class SearchTitlesHandler : IRequestHandler<SearchTitleListQueryParams, List<TitleDto>>
{
    private readonly TitleRepository _titleRepository;
    
    public SearchTitlesHandler(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }
    
    public async Task<List<TitleDto>> Handle(SearchTitleListQueryParams request, CancellationToken cancellationToken)
    {
        return await _titleRepository.SearchTitles(request.Skip, request.genres, request.OrderBy, true,
            request.Limit);
    }
}