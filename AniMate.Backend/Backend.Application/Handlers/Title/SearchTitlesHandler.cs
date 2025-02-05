using Backend.Application.Models.Title;
using Backend.Domain.Models.Anime;
using MediatR;

namespace Backend.Application.Handlers.Title;

public class SearchTitlesHandler : IRequestHandler<GetTitleListQueryParams, List<TitleDto>>
{
    public Task<List<TitleDto>> Handle(GetTitleListQueryParams request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}