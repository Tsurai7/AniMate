namespace Backend.AnilibriaWorker.Models; 

public record ReleaseDto
{
    string? Id { get; init; }
    
    string? Code { get; init; }
    
    string? Ordinal { get; init; }
    
    public NamesDto? Names { get; init; }
}