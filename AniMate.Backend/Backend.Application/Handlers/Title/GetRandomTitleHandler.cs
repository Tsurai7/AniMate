using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public record GetRandomTitleRequest : IRequest<TitleDto>;

public class GetRandomTitleHandler : IRequestHandler<GetRandomTitleRequest, TitleDto>
{
    private readonly TitleRepository _titleRepository;

    public GetRandomTitleHandler(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }
    
    public async Task<TitleDto> Handle(GetRandomTitleRequest request, CancellationToken ctx)
    {
        var title = await _titleRepository.GetRandomTitle(ctx);
        return title;
    }
}