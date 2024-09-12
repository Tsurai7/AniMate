namespace Adapter.Domain.Models.Responses;

public class ReleaseDto
{
    public string Id { get; init; }
        
    public string Code { get; init; }
        
    public string Ordinal { get; init; }
        
    public NamesDto Names { get; init; }
}