using Backend.Domain.Models.Anime;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers;

public class GetTitleCommand : IRequest<TitleDto>
{
    public string Code { get; set; }
}

public class GetTitleHandler : IRequestHandler<GetTitleCommand, TitleDto>
{
    private readonly AnimeRepository _animeRepository;

    public GetTitleHandler(AnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }
    
    public async Task<TitleDto> Handle(GetTitleCommand request, CancellationToken cancellationToken)
    {
        var title = await _animeRepository.GetTitleByCodeAsync(request.Code, cancellationToken);

        return title;
    }
}