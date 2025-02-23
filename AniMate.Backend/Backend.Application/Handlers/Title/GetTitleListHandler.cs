using Backend.Application.Models.Title;
using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public class GetTitleListHandler : IRequestHandler<GetTitleListQueryParams, List<TitleDto>>
{
    private readonly TitleRepository _titleRepository;
    
    public GetTitleListHandler(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }
    
    public async Task<List<TitleDto>> Handle(GetTitleListQueryParams request, CancellationToken ctx)
    {
        return await _titleRepository.GetTitles(request.Limit, request.Skip, ctx);
    }
}