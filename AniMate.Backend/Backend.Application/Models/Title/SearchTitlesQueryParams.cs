using Backend.Domain.Models.Anime;
using MediatR;

namespace Backend.Application.Models.Title;

public class SearchTitlesQueryParams : IRequest<List<TitleDto>>
{
}