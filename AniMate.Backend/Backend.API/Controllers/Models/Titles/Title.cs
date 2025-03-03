using Backend.Domain.Models.Anime;

namespace Backend.API.Controllers.Models.Titles;

public class Title
{
    public int Id { get; set; }
    public string Code { get; set; }
    public NamesDto Names { get; set; }
    public List<FranchiseDto>? Franchises { get; set; }
    public StatusDto Status { get; set; }
    public PostersDto Posters { get; set; }
    public TypeDto? Type { get; set; }
    public List<string> Genres { get; set; }
    public SeasonDto Season { get; set; }
    public string RuDescription { get; set; }
    public PlayerDto Player { get; set; }
}