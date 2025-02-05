using Backend.Application.Models.Title;
using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Title;

public class GetTitleHandler : IRequestHandler<GetTitleQueryParams, TitleDto>
{
    private readonly AnimeRepository _animeRepository;

    public GetTitleHandler(AnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }
    
    public async Task<TitleDto> Handle(GetTitleQueryParams request, CancellationToken cancellationToken)
    {
        var title = await _animeRepository.GetTitleByCodeAsync(request.Code, cancellationToken);

        return title;
    }
}