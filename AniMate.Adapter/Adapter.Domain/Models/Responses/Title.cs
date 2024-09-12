namespace Adapter.Domain.Models.Responses;

public class Title
{
    public string Id { get; init; }

    public string Code { get; init; }
    
    public NamesDto Names { get; init; }

    public List<FranchiseDto> Franchises { get; init; }
    
    public StatusDto Status { get; init; }

    public PostersDto Posters { get; init; }
    
    public TypeDto Type { get; init; }

    public List<string> Genres { get; init; }
    
    public SeasonDto SeasonDto { get; init; }
    
    public string RuDescription { get; init; }

    public long InFavorites { get; init; }
    
    public PlayerDto Player { get; init; }
}