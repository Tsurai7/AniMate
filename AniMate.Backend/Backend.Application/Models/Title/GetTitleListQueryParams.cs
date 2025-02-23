using Backend.Domain.Models.Anime;
using MediatR;

namespace Backend.Application.Models.Title;

public record GetTitleListQueryParams(int Skip, int Limit) : IRequest<List<TitleDto>>;