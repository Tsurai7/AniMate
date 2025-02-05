using Backend.Domain.Models.Anime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Models.Title;

public class GetTitleQueryParams : IRequest<TitleDto>
{
    [FromQuery(Name = "code")]
    public string Code { get; init; }
}