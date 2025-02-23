using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public record GetTitlesUpdatesQueryParams(int Skip, int Limit) : IRequest<List<TitleDto>>;

public class GetTitlesUpdatesHandler : IRequestHandler<GetTitlesUpdatesQueryParams, List<TitleDto>>
{
    private readonly TitleRepository _titleRepository;
    
    public GetTitlesUpdatesHandler(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }
    
    public async Task<List<TitleDto>> Handle(GetTitlesUpdatesQueryParams request, CancellationToken ctx)
    {
        return await _titleRepository.GetTitles(request.Limit, request.Skip, ctx);
    }
}