using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Models.Title;

public class GetTitleQueryParams
{
    [FromQuery(Name = "code")]
    public string Code { get; init; }
}