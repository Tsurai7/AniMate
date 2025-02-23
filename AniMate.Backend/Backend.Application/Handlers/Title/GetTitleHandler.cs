using Backend.Application.Models.Title;
using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public class GetTitleHandler : IRequestHandler<GetTitleQueryParams, TitleDto>
{
    private readonly TitleRepository _titleRepository;

    public GetTitleHandler(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }
    
    public async Task<TitleDto> Handle(GetTitleQueryParams request, CancellationToken ctx)
    {
        var title = await _titleRepository.GetTitleByCodeAsync(request.Code, ctx);
        return title;
    }
}